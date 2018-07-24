using System;
using System.ComponentModel;
using System.Windows;

namespace POC
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Easy : Window
    {
        private string t, d;
        private DateTime dt = DateTime.Now;
        private bool close;
        private static Easy instance;

        private Easy()
        {
            InitializeComponent();


            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            // Makes window Full Screen
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;

            char[] MyChar = { 'P', 'M', 'A' };
            d = String.Format("{0}, {1} {2}", dt.DayOfWeek, dt.ToString("MMMM"), dt.Day);
            t = String.Format("{0:t}", dt).TrimEnd(MyChar);
            //labelName.FontWeight= labelDate.FontWeight = FontWeights.Bold;
       

            labelName.Content = t;
            labelDate.Content = d;
            this.Show();
        }

        public static Easy ez
        {
            get
            {
                if(instance == null)
                {
                    instance = new Easy();
                }
                return instance;
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = close ? false : true;
        }

        private void PW_Changed_Handler(object sender, RoutedEventArgs e)
        {
            if (PIN.Password.Length > 0)
            {
                watermark.Visibility = Visibility.Hidden;
            }
            else
            {
                watermark.Visibility = Visibility.Visible;
            }
        }


        /// <Return_OnClick>
        /// Returns to the Check Window
        /// Deletes the current instance;
        /// Enables the close boolean to enable closure
        /// Reveals the Check Window
        /// Closes current window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Return_OnClick(object sender, RoutedEventArgs e)
        {
            if(PIN.Password.Length == 4)
            {
                instance = null;
                close = true;
                MainWindow.check.Show();
                this.Close();

            }
        }
    }
}
