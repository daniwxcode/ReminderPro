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

        public static string EmailTemplate()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Mail.html");
            if (File.Exists(path))
                return File.ReadAllText(path);
            return string.Empty;
        }
    }
}