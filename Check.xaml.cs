using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Design;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Shapes;
using System.ComponentModel;

namespace POC
{
    /// Map implementation of Bing Maps API 
    /// Checks user current position
    /// 3 PreLoaded geofences available: HP Houston, Vancouver, and Palo Alto
    /// 
    /// One unique geofence in use at a time
    ///
    /// PIP checks to see if point is inside the given polygon (in this case, the geofence)
    /// Checks network connection to see if the user is registered as a company workstation
    /// if both are true, implement easy login (Easy) with one authentication
    /// else, implement hard login (Difficult) with MFA.
    /// 
    /// Map is draggable, and can create polygons dynamicaly through mouse doubleclick
    /// and pressing the Create a Polygon.
    public partial class Check : Window
    {

        private bool closee = false;
        private static Check instance;
        private Check()
        {
            InitializeComponent();
            getNetwork();
            //Choose better names for variables> like inNetwork
            Network.Text = global.Net ? correct : wrong;
            coordinate.Text = global.GPS ? inside : outside;
            
            myMap.Mode = new AerialMode(true);
            myMap.Focus();
            SetUpNewPolygon();

            updates(global.Houston);

            myMap.ViewChangeOnFrame += new EventHandler<MapEventArgs>(viewMap_ViewChangeOnFrame);
            // Adds location points to the polygon for every single mouse click
            myMap.MouseDoubleClick += new MouseButtonEventHandler(
            MyMap_MouseDoubleClick);
            // Adds the layer that contains the polygon points
            NewPolygonLayer.Children.Add(polygonPointLayer);
            // Makes window Full Screen
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;
            this.Show();
        }
        // The user defined polygon to add to the map.
        MapPolygon newPolygon = null;
        // The map layer containing the polygon points defined by the user.
        MapLayer polygonPointLayer = new MapLayer();

        LocationConverter locConverter = new LocationConverter();
        string inside = " You are inside the company's geofence";
        string outside = " You are outside the company's geofence";
        string correct = " You are in the correct network";
        string wrong = " You are in the wrong network";

        /// Singleton implementation
        public static Check checkk
        {
            get{
                if(instance == null)
                {
                    instance = new Check();
                }
                return instance;
            }
           
        }

        /// Checks the network
        private void getNetwork()
        {

            var x = System.Net.NetworkInformation.
                IPGlobalProperties.GetIPGlobalProperties().DomainName;
            global.Net = (global.DomainName == x);

        }



        /// Updates the Map
        private void viewMap_ViewChangeOnFrame(object sender, MapEventArgs e)
        {
            Map map = sender as Map;
            if (map != null)
            {
                Location mapCenter = map.Center;
                Lat.Text = "Latitude:";
                Lng.Text = "Longitude:";
                txtLatitude.Text = string.Format(CultureInfo.InvariantCulture,
                                  "{0:F5}", mapCenter.Latitude);
                txtLongitude.Text = string.Format(CultureInfo.InvariantCulture,
                    "{0:F5}", mapCenter.Longitude);


            }
        }

        /// Adds new polygon to the Map
        void addNewPolygon()
        {
            MapPolygon polygon = new MapPolygon();
            polygon.Fill = new SolidColorBrush(Colors.Blue);
            polygon.Stroke = new SolidColorBrush(Colors.Green);
            polygon.StrokeThickness = 5;
            polygon.Opacity = 0.7;
            polygon.Locations = global.Locations;

            for (int x = myMap.Children.Count - 1; x >= 0; x--)
            {
                if (myMap.Children[x].GetType() == typeof(MapPolygon))
                {
                    myMap.Children.Remove(myMap.Children[x]);
                }
            }
            myMap.Children.Add(polygon);
        }

        /// Shows Current Location
        /// Checks to see if inside Geofence
        private void CurrentView_Click(object sender, RoutedEventArgs e)
        {
            myMap.SetView(global.Coord, 16);

            global.GPS = PIP();
            coordinate.Text = global.GPS ? inside : outside;

        }
        /// Changes loading styles
        private void AnimationLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cbi = (ComboBoxItem)(((ComboBox)sender).SelectedItem);
            string v = cbi.Content as string;
            if (!string.IsNullOrEmpty(v) && myMap != null)
            {
                AnimationLevel newLevel = (AnimationLevel)Enum.Parse(typeof(AnimationLevel), v, true);
                myMap.AnimationLevel = newLevel;
            }
        }


        /// Goes to lock screen
        private void LogIn_Click(object sender, RoutedEventArgs e)
        {

            closee = true;
            Lock welcome_screen = Lock.lockit;
            Hide();           
        }
        /// Goes to the Vancouver, PaloAlto, or Houston
        private void Vancouver_click(object sender, RoutedEventArgs e)
        {
            updates(global.Vancouver);

            
            myMap.SetView(new Location(45.613215, -122.501084), 18);
        }
        private void PaloAlto_Click(object sender, RoutedEventArgs e)
        {
            updates(global.PaloAlto);
            myMap.SetView(new Location(37.412086, -122.148421), 18);
        }
        private void Houston_Click(object sender, RoutedEventArgs e)
        {
            updates(global.Houston);

            myMap.SetView(new Location(29.990102, -95.582727), 18);
        }

        /// Updates the Messages
        private void updates(LocationCollection a)
        {
            global.Locations = a;
            global.GPS = PIP();

            if (global.GPS)
            {
                coordinate.Text = inside;
                coordinate.Foreground = Brushes.Green;
            }
            else
            {
                coordinate.Text = outside;
                coordinate.Foreground = Brushes.Red;
            }

            if (global.Net)
            {
                Network.Foreground = Brushes.Green;
            }
            else
            {
                Network.Foreground = Brushes.Red;
            }
            addNewPolygon();
            Pushpin pin = new Pushpin() { Location = global.Coord };

            for (int x = myMap.Children.Count - 1; x >= 0; x--)
            {
                if (myMap.Children[x].GetType() == typeof(Pushpin))
                {
                    myMap.Children.Remove(myMap.Children[x]);
                }
            }
            myMap.Children.Add(pin);

        }

        /// Point in Polygon Method
        /// Checks the GPS coordinate against the collection of coordinates
        private static bool PIP()
        {
            bool inside = false;
            Location x = global.Locations[global.Locations.Count - 1];
            foreach (Location e in global.Locations)
            {
                double d1 = (global.Coord.Longitude - e.Longitude) * (x.Latitude - e.Latitude);
                double d2 = (global.Coord.Latitude - e.Latitude) * (x.Longitude - e.Longitude);

                if (global.Coord.Longitude < x.Longitude)
                {
                    if (e.Longitude <= global.Coord.Longitude)
                    {
                        if (d1 > d2) { inside = !inside; }
                    }
                }
                else if (global.Coord.Longitude < e.Longitude)
                {
                    if (d1 < d2) { inside = !inside; }
                }

                x = e;
            }
            return inside;
        }
        private void SetUpNewPolygon()
        {
            newPolygon = new MapPolygon();
            // Defines the polygon fill details
            newPolygon.Locations = new LocationCollection();
            newPolygon.Fill = new SolidColorBrush(Colors.Blue);
            newPolygon.Stroke = new SolidColorBrush(Colors.Green);
            newPolygon.StrokeThickness = 3;
            newPolygon.Opacity = 0.8;
            //Set focus back to the map so that +/- work for zoom in/out
            myMap.Focus();
        }

        /// When double clicks happen, 
        /// represent the click with a box on the clicked location
        private void MyMap_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            // Creates a location for a single polygon point and adds it to
            // the polygon's point location list.
            Point mousePosition = e.GetPosition(this);
            //Convert the mouse coordinates to a location on the map
            Location polygonPointLocation = myMap.ViewportPointToLocation(
                mousePosition);
            newPolygon.Locations.Add(polygonPointLocation);

            // A visual representation of a polygon point.
            Rectangle r = new Rectangle();
            r.Fill = new SolidColorBrush(Colors.Red);
            r.Stroke = new SolidColorBrush(Colors.Yellow);
            r.StrokeThickness = 1;
            r.Width = 8;
            r.Height = 8;

            // Adds a small square where the user clicked, to mark the polygon point.
            polygonPointLayer.AddChild(r, polygonPointLocation);
            //Set focus back to the map so that +/- work for zoom in/out
            myMap.Focus();
        }

        /// Instantiate the Polygon that is created from the buttons
        private void btnCreatePolygon_Click(object sender, RoutedEventArgs e)
        {
            //If there are two or more points, add the polygon layer to the map
            if (newPolygon.Locations.Count >= 2)
            {
                // Removes the polygon points layer.
                polygonPointLayer.Children.Clear();

                //show the polygon on map
                updates(newPolygon.Locations);
                
                SetUpNewPolygon();
            }
        }
        /// Disable the closing of the window
        /// Dependent on boolean closee
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = closee ? false : true;
        }
        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            instance = null;
            closee = true;
            this.Close();

        }
    }
}
