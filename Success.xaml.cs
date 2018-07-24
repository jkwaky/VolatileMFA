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
    /// <summary>
    /// Interaction logic for Success.xaml
    /// </summary>
    public partial class Success : Window
    {
        private static Success instance;

        private Success()
        {
            InitializeComponent();

            // Makes window Full Screen
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;


            Show();
        }

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


    }
}
