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
using System.Windows.Shapes;

namespace SimpleLogWindow
{
    public partial class LogConsole : Window
    {
        private MainWindow mainwindow;

        public LogConsole() { }

        public LogConsole(MainWindow mainwindow)
        {
            InitializeComponent();

            this.mainwindow = mainwindow;
        }

        public void Log(string text)
        {
            this.OutputBox.AppendText(text + "\n");
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
            base.OnClosing(e);
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
            return;
        }
    }
}
