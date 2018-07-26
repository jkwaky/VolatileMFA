using System;
using System.ComponentModel;
using System.Windows;

namespace POC
{
    /// Interaction logic for Window1.xaml
    public partial class Easy : Window
    {
        private bool close;
        private static Easy instance;

        private Easy()
        {
            InitializeComponent();

            // Makes window Full Screen
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;

             this.Show();
        }

        /// Singleton implementation of Easy class
        public static Easy ez
        {
            get
            {
                if (instance == null)
                {
                    instance = new Easy();
                }
                return instance;
            }
        }

        /// Override the close button and action
        /// Dependent on private bool close
        /// disable the closing function when close is false
        /// enable closing when close is true
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = close ? false : true;
        }

        /// Shows the watermark when nothing is typed
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



        /// Returns to the Check Window
        /// Deletes the current instance;
        /// Enables the close boolean to enable closure
        /// Reveals the Check Window
        /// Closes current window

        private void Return_OnClick(object sender, RoutedEventArgs e)
        {
            if(PIN.Password.Length == 4)
            {
                instance = null;
                close = true;
                Success LoggedIn = Success.succeed;
                this.Close();

            }
        }
    }
}
