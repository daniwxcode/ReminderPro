using System;

namespace Services
{
    public class AppSettingsParser
    {
        private static readonly object _lock = new Object();

        public static AppSettings Settings;

        public static void AppSettings(string appSettingsJsonFilePath = null)
        {
            if (appSettingsJsonFilePath == null)
            {
                appSettingsJsonFilePath = System.IO.Path.Combine(System.AppContext.BaseDirectory, "ReminderSettings.json");
            }

            var json = string.Empty;
            lock (_lock)
            {
                json = System.IO.File.ReadAllText(appSettingsJsonFilePath);
            }
            Settings = Newtonsoft.Json.JsonConvert.DeserializeObject<AppSettings>(json);
        }

        public static void AppSettings(AppSettings appSettings, string appSettingsJsonFilePath = null)
        {
            if (appSettingsJsonFilePath == null)
            {
                appSettingsJsonFilePath = System.IO.Path.Combine(System.AppContext.BaseDirectory, "ReminderSettings.json");
            }

            string output = Newtonsoft.Json.JsonConvert.SerializeObject(appSettings, Newtonsoft.Json.Formatting.Indented);
            lock (_lock)
            {
                System.IO.File.WriteAllText(appSettingsJsonFilePath, output);
            }
            Settings = appSettings;
        }
    }
}