using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Design;
using System;
using System.Device.Location;
using System.Windows;

namespace POC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Check check;
     
        public MainWindow()
        {
            InitializeComponent();
            Begin.Visibility = Visibility.Hidden;
            Setting.Visibility = Visibility.Hidden;
            CLocation.GetLocationEvent();
            CLocation.MainWin = this;
            LocationConverter locConverter = new LocationConverter();
            //set default parameters
            global.DomainName = "auth.hpicorp.net";
            string x = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
            geoIp2.Text = x;
            //domainName.Text = global.Coord.ToString();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;


            global.Locations = new LocationCollection()
            {
                new Location(29.991445, -95.585568),
                new Location(29.991389, -95.580096),
                new Location(29.988248, -95.579860),
                new Location(29.987988, -95.585375) };
       
        }


        /// <summary>
        /// REMOVE THIS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            Setting set = POC.Setting.Sett;
        }

        private void Begin_Click(object sender, RoutedEventArgs e)
        {

            check = POC.Check.checkk;
            Close();

        }

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
                bool started = watcher.TryStart(false, TimeSpan.FromMilliseconds(2000));
            }
            static void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
            {
                global.Coord = new Location(e.Position.Location.Latitude, e.Position.Location.Longitude);
                
                MainWin.Begin.Visibility = Visibility.Visible;
                MainWin.Setting.Visibility = Visibility.Visible;
                MainWin.geoIp2.Text = global.Coord.ToString();
            }

           

        }
    }
}
