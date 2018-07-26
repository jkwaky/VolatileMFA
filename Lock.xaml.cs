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
using System.Windows.Threading;

namespace POC
{
    /// Initial screen for Windows lock screen (simulated)

    public partial class Lock : Window
    {
        private static Lock instance;
        private DateTime dt = DateTime.Now;
        private bool close;
        private string t, d;
        private Lock()
        {


            InitializeComponent();
            // Makes window Full Screen
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;


            DispatcherTimer dayTimer = new DispatcherTimer();
            dayTimer.Interval = TimeSpan.FromMilliseconds(500);
            dayTimer.Tick += new EventHandler(dayTimer_Tick);
            dayTimer.Start();


            Show();
        }
        public static Lock lockit
        {

            get {
                if(instance == null)
                {
                    instance = new Lock();
                }
                return instance;
            }
        }

        private void Leave(object sender, MouseButtonEventArgs e)
        {
            end();
        }

        private void Leave(object sender, KeyEventArgs e)
        {
            end();
        }

        void dayTimer_Tick(object sender, EventArgs e)
        {

            // parse the datetime object to get signficant information
            // string d is for date
            // string t is for time
            // upates the labels to show correct time

            dt = DateTime.Now;
            char[] MyChar = { 'P', 'M', 'A' };
            d = String.Format("{0}, {1} {2}", dt.DayOfWeek, dt.ToString("MMMM"), dt.Day);
            t = String.Format("{0:t}", dt).TrimEnd(MyChar);

            labelName.Content = t;
            labelDate.Content = d;
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = close ? false : true;
        }
        private void end()
        {
            instance = null;
            close = true;
            if (global.GPS)
            {
                Easy easy = Easy.ez;
            }
            else
            {
                Difficult difficult = Difficult.diff;
            }
            Close();
        }
    }
}
