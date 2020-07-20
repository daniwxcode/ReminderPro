using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing.Tree;

namespace Dashboard.Models
{
    public class AppSettingsParser
    {
        private readonly IConfiguration _configuration;

        public AppSettingsParser(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AppSettings GetAppSettings()
        {
            AppSettings appSettings = new AppSettings();
            appSettings.SourceDB = _configuration["Params:Source"];
            appSettings.DaysEcart = AppConfig.Ecart;
            appSettings.InstitutionName = _configuration["Params:Nom"];
            appSettings.Signature = _configuration["Params:Signature"];
            appSettings.User = _configuration["InfoBipSettings:User"];
            appSettings.Password = _configuration["InfoBipSettings:Password"];
            appSettings.ApiUrl = _configuration["InfoBipSettings:ApiUrl"];
            appSettings.Sender = _configuration["InfoBipSettings:Sender"];
            appSettings.CountryCode = _configuration["InfoBipSettings:CountryCode"];
            return appSettings;
        }

        public static void SetAppSettingValue(AppSettings appSettings, string appSettingsJsonFilePath = null)
        {
            if (appSettingsJsonFilePath == null)
            {
                appSettingsJsonFilePath = System.IO.Path.Combine(System.AppContext.BaseDirectory, "appsettings.json");
            }

            var json = System.IO.File.ReadAllText(appSettingsJsonFilePath);

            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(json);

            Type classType = typeof(AppSettings);
            foreach (PropertyInfo field in classType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var old = jsonObj[field.Name] ?? "";
                var @new = field.GetValue(appSettings).ToString() ?? "";
                jsonObj[field.Name] = @new;
            }

            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);

            System.IO.File.WriteAllText(appSettingsJsonFilePath, output);
        }
    }
}