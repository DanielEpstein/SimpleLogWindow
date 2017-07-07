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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleLogWindow
{
    public partial class MainWindow : Window
    {
        int count = 0;

        public MainWindow()
        {
            InitializeComponent();
            LogConsole.WriteLine("MainWindow ctor");

            if (SettingsManger.OpenLogConsoleOnLaunch)
            {
                LogConsole.Visible();
            }
            OpenLogConsoleOnLaunchCheckBox.IsChecked = SettingsManger.OpenLogConsoleOnLaunch;
        }

        private void buttonLogConsole_Click(object sender, RoutedEventArgs e)
        {
            LogConsole.ToggleVisibility();
        }

        private void AddLogItem_Click(object sender, RoutedEventArgs e)
        {
            count++;
            LogConsole.WriteLine("Log Item: " + count);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            LogConsole.Close();
        }

        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsManger.Save();
        }

        private void OpenLogConsoleOnLaunchCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SettingsManger.OpenLogConsoleOnLaunch = true;
        }

        private void OpenLogConsoleOnLaunchCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SettingsManger.OpenLogConsoleOnLaunch = false;
        }
    }
}
