using Dokimion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater5
{
    public class Settings
    {
        public string username = "";
        public string server = "";
        public string repo = "";
        public bool useHttps = false;
        public bool oneFolderForAllProjects = false;
    }


    public class Data
    {
        public bool LoggedIn;
        public Dokimion.Dokimion Dokimion;
        public List<Project>? Projects;
        public int ProjectIndex;
        public Settings Settings;

        public Data()
        {
            Dokimion = new("", false);
            Settings = new();
        }

        public string? GetSettings()
        {
            string settingsJson = "";
            try
            {
                settingsJson = File.ReadAllText("Updater.json");
            }
            catch
            {
                // Ignore errors
            }
            Settings = new Settings();
            if (false == string.IsNullOrEmpty(settingsJson))
            {
                try
                {
                    Settings? trySettings = JsonConvert.DeserializeObject<Settings>(settingsJson);
                    if (trySettings != null)
                    {
                        Settings = trySettings;
                    }
                }
                catch
                {
                    return "Cannot decode Updater.json which contains: \r\n" + settingsJson;
                }
            }
            return null;
        }

        public void SaveSettings()
        {
            try
            {
                File.WriteAllText("Updater.json", JsonConvert.SerializeObject(Settings));
            }
            catch
            {
                ; // Ignore errors
            }
        }


    }
}
