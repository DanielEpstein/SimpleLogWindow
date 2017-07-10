using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

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
            myLogConsole.outputter.WriteLine(str);
        }

        public static void Write(string str)
        {
            myLogConsole.outputter.Write(str);
        }
    }

    public partial class LogConsoleWindow : Window
    {
        public TextBoxOutputter outputter;
        public bool CloseForReal = false;

        // ctor
        public LogConsoleWindow()
        {
            InitializeComponent();
            outputter = new TextBoxOutputter(OutputBox);
            outputter.WriteLine("LogConsoleWindow ctor");
        }

        // Toggle LogConsoleWindow Visibility
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

        // ScrollViewer Auto Scroller
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

        // Only close LogConsoleWindow from main window, otherwise collapse
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
