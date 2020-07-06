using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Microsoft.Extensions.Configuration;

namespace DAL
{
    public static class AppConfig
    {
        public static IConfiguration Config()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            return configurationBuilder.Build();
        }

        public static string GetObjProperty(this object obj, string property)
        {
            return obj.GetType().GetProperty("property").ToString();
        }
    }
}