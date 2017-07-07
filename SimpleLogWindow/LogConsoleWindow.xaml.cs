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

namespace SimpleLogWindow
{
    public static class LogConsole
    {
        private static LogConsoleWindow myLogConsole = new LogConsoleWindow();

        public static void ToggleVisibility()
        {
            myLogConsole.ToggleVisibility();
        }

        public static void Visible()
        {
            myLogConsole.Visibility = Visibility.Visible;
        }

        public static void Close()
        {
            myLogConsole.CloseForReal = true;
            myLogConsole.Close();
        }

        public static void WriteLine(string str)
        {
            myLogConsole.WriteLine(str);
        }

        public static void Write(string str)
        {
            myLogConsole.Write(str);
        }
    }

    public partial class LogConsoleWindow : Window
    {
        public bool CloseForReal = false;

        public LogConsoleWindow()
        {
            InitializeComponent();
            WriteLine("LogConsoleWindow ctor");

            
        }

        public void ToggleVisibility()
        {
            if (this.Visibility != Visibility.Visible)
            {
                this.Visibility = Visibility.Visible;
                this.Scroller.ScrollToEnd();
                return;
            }
            this.Visibility = Visibility.Collapsed;
        }



        public void WriteLine(string text)
        {
            this.OutputBox.AppendText(text + "\n");
        }

        public void Write(string text)
        {
            this.OutputBox.AppendText(text);
        }

        private void scrollviewer_Messages_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer sv = sender as ScrollViewer;
            bool AutoScrollToEnd = true;
            if (sv.Tag != null)
            {
                AutoScrollToEnd = (bool)sv.Tag;
            }
            if (e.ExtentHeightChange == 0)
            {
                AutoScrollToEnd = sv.ScrollableHeight == sv.VerticalOffset;
            }
            else
            {
                if (AutoScrollToEnd)
                {
                    sv.ScrollToEnd();
                }
            }
            sv.Tag = AutoScrollToEnd;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            if(!CloseForReal)
            {
                e.Cancel = true;
            }
            base.OnClosing(e);
        }
    }
}
