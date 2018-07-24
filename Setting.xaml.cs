using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Design;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace POC
{
    /// <summary>
    /// Interaction logic for Setting.xaml
    /// </summary>
    /// 
    public class Singleton
    {
        private static Singleton instance;

        private Singleton() { }

        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }
    }
    public partial class Setting : Window
    {
        private bool close = false;

        private static Setting instance;
        private Setting()
        {
            InitializeComponent();
            DataContext = this;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.Show();
        }
        public static Setting Sett
        {
            get
            {
                if (instance == null)
                {
                    instance = new Setting();
                }
                return instance;
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = close ? false : true;
        }
        LocationConverter locConverter = new LocationConverter();

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            myPanel.Children.Add(new TextBox());
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if(myPanel.Children.Count> 0)
            {
                myPanel.Children.RemoveAt(myPanel.Children.Count - 1);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            global.DomainName = DomainName.Text;
            var textboxes = myPanel.Children;
            LocationCollection loc = new LocationCollection();
            foreach (TextBox item in textboxes)
            {
                loc.Add((Location)locConverter.ConvertFrom(item.Text));
            }
            global.Locations = loc;

            instance = null;
            close = true;
            this.Close();
        }
    }
}
