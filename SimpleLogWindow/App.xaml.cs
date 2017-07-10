using System.Windows;

namespace SimpleLogWindow
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        App()
        {
            LogConsole.WriteLine("App ctor");
            SettingsManger.Load();
        }
    }

    
}
