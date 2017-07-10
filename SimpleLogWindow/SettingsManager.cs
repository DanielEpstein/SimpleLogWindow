using System;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;

namespace SimpleLogWindow
{
    public static class SettingsManger
    {
        private static XMLDataManager xmldm;

        public static bool OpenLogConsoleOnLaunch
        {
            get { return xmldm.Settings.OpenLogConsoleOnLaunch; }
            set { xmldm.Settings.OpenLogConsoleOnLaunch = value; }
        }

        public static string ReturnSettingAsStrings()
        {
            string output =
                "----------<Loaded Settings>----------\n" +
                "OpenLogConsoleOnLaunch: " + OpenLogConsoleOnLaunch + "\n" +
                "-------------------------------------\n";
            return output;
        }

        public static void Load()
        {
            xmldm = new XMLDataManager();
            LogConsole.Write(ReturnSettingAsStrings());
        }

        public static void Save()
        {
            xmldm.SaveSettings();
        }
    }

    public class XMLDataManager
    {
        public string SettingsPath = GetExecutingDirectoryName() + "\\" + GetExecutingFileName() + ".xml";
        public XMLData Settings { get; private set; }
        

        public XMLDataManager()
        {
            LogConsole.WriteLine("SettingsManager ctor");
            Settings = new XMLData();

            if (File.Exists(SettingsPath))
            {
                LogConsole.WriteLine("Found settings.xml, Reading");
                this.Settings = Settings.Read(SettingsPath);
            }
            else
            {
                LogConsole.WriteLine("Missing(" + SettingsPath + ") Creating");
                Settings.Save(SettingsPath);
            }
        }

        public static string GetExecutingFileName()
        {
            return Assembly.GetEntryAssembly().GetName().Name;
        }

        public static string GetExecutingDirectoryName()
        {
            Uri location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
            return new FileInfo(location.AbsolutePath).Directory.FullName;
            
        }

        public void SaveSettings()
        {
            LogConsole.WriteLine("Saving settings.xml");
            Settings.Save(SettingsPath);
        }
    }

    public class XMLData
    {
        public bool OpenLogConsoleOnLaunch { get; set; }

        public void Save(string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                XmlSerializer xmls = new XmlSerializer(typeof(XMLData));
                xmls.Serialize(sw, this);
            }
        }
        public XMLData Read(string filename)
        {
            using (StreamReader sw = new StreamReader(filename))
            {
                XmlSerializer xmls = new XmlSerializer(typeof(XMLData));
                return xmls.Deserialize(sw) as XMLData;
            }
        }
    }
}
