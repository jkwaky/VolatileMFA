using System.ComponentModel;
using System.Windows;

namespace POC
{
    /// Simulation of MFA login
    /// Triggered through pressing the log in button
    /// AND not being inside the GeoFence
    public partial class Difficult : Window
    {
        private bool close = false;
        private static Difficult instance;
        private bool pinFin = false;
        private bool passFin = false;
        private bool fingerFin = false;
        private Difficult()
        {
            InitializeComponent();
            // Makes window Full Screen
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            Show();
        }

        /// Singleton implementation
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
        /// Called everytime the password is changed
        /// Checks to see if the password box has any characters in it
        /// if no characters, have it show the watermark
        /// if there are characters, don't show watermark
        private void PW_Changed_Handler(object sender, RoutedEventArgs e)
        {
            //if password is visible
            //check watermark with password
            if (Password.Visibility == Visibility.Visible)
            {
                if(0 < Password.Password.Length)
                {
                    watermark.Visibility = Visibility.Hidden;
                }
                else
                {
                    watermark.Visibility = Visibility.Visible;
                }
            }
            else
            {
                if(0 < PIN.Password.Length)
                {
                    watermark.Visibility = Visibility.Hidden;
                }
                else
                {
                    watermark.Visibility = Visibility.Visible;
                }
            }
        }

        /// Disables window close()
        /// Dependent on bool enable close
        /// if(close) then close
        /// if(!close) then don't enable close
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = close ? false : true;
        }


        /// Called when Finger Print icon is pressed
        /// Make PIN box and enter icon invisible
        /// Make PW box and enter icon invisible
        /// Make watermark invisible
        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            Arrow.Visibility = Visibility.Hidden;
            fingerFin = true;
            watermark.Content = "";
            PrintButton.Visibility = Visibility.Hidden;
            hide_pass();
            hide_pin();
            if(fingerFin && pinFin)
            {
                end();
            }
        }
        /// Refractoring for code reuse
        private void hide_pin()
        {
            PIN.Visibility = Visibility.Hidden;
            ReturnPin.Visibility = Visibility.Hidden;
        }

        /// Refractoring for code reuse
        private void show_pin()
        {
            PIN.Visibility = Visibility.Visible;
            ReturnPin.Visibility = Visibility.Visible;
        }

        /// Refractoring for code reuse
        private void hide_pass()
        {
            Password.Visibility = Visibility.Hidden;
            ReturnPass.Visibility = Visibility.Hidden;
        }
        /// Refractoring for code reuse
        private void show_pass()
        {
            Password.Visibility = Visibility.Visible;
            ReturnPass.Visibility = Visibility.Visible;
        }
        /// Called when PIN icon is pressed
        /// Make PW passwordbox and enter icon invisible
        /// Make PIN box and icon visible
        /// change watermark content to "PIN"
        private void PinButton_Click(object sender, RoutedEventArgs e)
        {
            Arrow.Visibility = Visibility.Visible;
            watermark.Content = "PIN";
            show_pin();
            hide_pass();
        }

        /// Called when PW icon is pressed
        /// Make PIN passwordbox and enter icon invisible
        /// Make PW box and icon visible
        /// change watermark content to "Password"
        private void PWButton_Click(object sender, RoutedEventArgs e)
        {
            Arrow.Visibility = Visibility.Visible;
            watermark.Content = "Password";
            show_pass();
            hide_pin();
        
        }

        // Closes this window
        private void end()
        {
            instance = null;
            close = true;
            Success LoggedIn = Success.succeed;
            Close();
        }

        /// Called when PIN is entered
        /// Checks if PIN length is acceptable
        /// Closes window if finger and Password is true
        /// if Passfin is not true go to password passwordbox
        /// if finger is not true do nothing
        private void ReturnPin_Click(object sender, RoutedEventArgs e)
        {
            Arrow.Visibility = Visibility.Visible;
            if (PIN.Password.Length == PIN.MaxLength)
            {
                pinFin = true;
                PinButton.Visibility = Visibility.Hidden;
                hide_pin();

                if (passFin && fingerFin)
                {
                    end();
                }
                else if(!passFin)
                {
                    show_pass();
                }
                else
                {
                    Arrow.Visibility = Visibility.Hidden;
                }
            }
        }

        /// Called when password is entered
        /// Checks if password length is acceptable
        /// Closes window if finger and pin is true
        /// if Pin is not true go to pin passwordbox
        /// if finger is not true do nothing
        private void ReturnPass_Click(object sender, RoutedEventArgs e)
        {
            if (Password.MaxLength == Password.Password.Length)
            {
                passFin = true;

                PWButton.Visibility = Visibility.Hidden;
                hide_pass();

                if (pinFin && fingerFin)
                {
                    end();
                }
                else if(!pinFin)
                {
                    show_pin();
                }
                else
                {
                    Arrow.Visibility = Visibility.Hidden;
                }
            }
        }

    }
}
