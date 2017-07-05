using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleLogWindow
{
    public partial class MainWindow : Window
    {
        int count = 0;
        LogConsole console = new LogConsole();

        public MainWindow()
        {
            InitializeComponent();
            StartLogConsole();
        }

        public void StartLogConsole()
        {
            MainWindow mainwindow = this;
            console = new LogConsole(mainwindow);
            // console.Left = this.Left + 0;
            // console.Top = this.Top + 0;
            console.Visibility = Visibility.Collapsed;
        }

        private void buttonLogConsole_Click(object sender, RoutedEventArgs e)
        {
            if (console.Visibility != Visibility.Visible)
            {
                console.Visibility = Visibility.Visible;
                console.Scroller.ScrollToEnd();
                return;
            }
            console.Visibility = Visibility.Collapsed;
        }

        private void AddLogItem_Click(object sender, RoutedEventArgs e)
        {
            count++;
            console.Log("Log Entry: " + count);
            
        }
    }
}
