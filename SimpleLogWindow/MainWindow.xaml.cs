using System.ComponentModel;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;

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

        private void TaskButton_Click(object sender, RoutedEventArgs e)
        {
            TaskManager.RunTask();
        }
    }

    public static class TaskManager
    {
        static int taskcount = 0;

        public static void RunTask()
        {
            taskcount++;
            Task.Run(() => DoSomething(taskcount));

        }

        static void DoSomething(int tc)
        {
            LogConsole.WriteLine("start of task " + tc);
            Thread.Sleep(5000);
            LogConsole.WriteLine("end of task " + tc);
        }
    }
}
