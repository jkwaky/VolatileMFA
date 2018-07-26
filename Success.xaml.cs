using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace POC
{
    /// Window to appear after a successful login
    /// Returns on any keyperss to the Check window
    public partial class Success : Window
    {
        private static Success instance;
        private bool close = false;

        private Success()
        {
            InitializeComponent();

            // Makes window Full Screen
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;
            Show();
        }


        /// Singleton implementation of Success class
        public static Success succeed
        {
            get
            {
                if(instance == null)
                {
                    instance = new Success();
                }
                return instance;
            }
        }
        /// Returns to the Check window
        /// Closes current window
        /// Called upon any keypress release

        void end()
        {
            instance = null;
            close = true;
            MainWindow.check.Show();
            this.Close();
        }


        private void Leave(object sender, MouseButtonEventArgs e)
        {
            end();
        }

        private void Leave(object sender, KeyEventArgs e)
        {
            end();
        }

    }
}
