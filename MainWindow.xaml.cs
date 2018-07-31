using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Design;
using System;
using System.Device.Location;
using System.Windows;

namespace POC
{
    /// Main Window
    /// Does NOT show up on the screen
    /// Buffer for the GPS coordinates to load
    /// Once it loads, open the Check Window
    public partial class MainWindow : Window
    {
        public static Check check;
     
        public MainWindow()
        {
            InitializeComponent();
            CLocation.GetLocationEvent();
            CLocation.MainWin = this;
            LocationConverter locConverter = new LocationConverter();

            //set default parameters
            global.DomainName = "auth.hpicorp.net";
            string x = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
            geoIp2.Text = x;
            Visibility = Visibility.Hidden;

            global.Locations = new LocationCollection()
            {
                new Location(29.991445, -95.585568),
                new Location(29.991389, -95.580096),
                new Location(29.988248, -95.579860),
                new Location(29.987988, -95.585375) };

            /// Immmediately start the Check window 
            /// Don't show this window
            /// This window is unnecessary


        }

        /// Sets up a watcher to watch for GPS coordinate changes
        /// Once GPS coordinate is received, close the Main Window
        public static class CLocation
        {
            public static MainWindow MainWin;
            public static GeoCoordinateWatcher watcher;
            private static string coordinates;
            public static string Coordinates { get => coordinates; set => coordinates = value; }

            public static void GetLocationEvent()
            {
               watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
               watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);
               bool wait = watcher.TryStart(false, TimeSpan.FromMilliseconds(2000));
            }
            /// wait until the coordinates are available before going into the program.
            static void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
            {
                global.Coord = new Location(e.Position.Location.Latitude, e.Position.Location.Longitude);
                
                // closes the window
                check = POC.Check.checkk;
                MainWin.Close();
            }

           

        }
    }
}
