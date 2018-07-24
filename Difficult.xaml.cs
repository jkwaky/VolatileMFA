using System.ComponentModel;
using System.Windows;

namespace POC
{
    /// <summary>
    /// Interaction logic for Difficult.xaml
    /// </summary>
    public partial class Difficult : Window
    {
        private bool close = false;
        private static Difficult instance;
        private Difficult()
        {
            InitializeComponent();
            // Makes window Full Screen
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            Show();
        }
        public static Difficult diff
        {
            get{
                if(instance == null)
                {
                    instance = new Difficult();
                }
                return instance;
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = close ? false : true;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            instance = null;
            close = true;
            
            this.Close();
        }
    }
}
