using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Microsoft.Extensions.Configuration;

namespace DAL
{
    public static class AppConfig
    {
        public static IConfiguration Config
        {
            get
            {
                var configurationBuilder = new ConfigurationBuilder();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

                configurationBuilder.AddJsonFile(path, false);
                return configurationBuilder.Build();
            }
            set
            {
                Config = value;
            }
        }

        public static string DataConnection
        {
            get
            {
                return Config["Params:DataConnection"];
            }
        }

        public static string Signature
        {
            get
            {
                return Config["Params:Signature"];
            }
        }

        public static string Sms
        {
            get
            {
                return Config["Params:Sms"];
            }
        }

        public static string Nom
        {
            get
            {
                return Config["Params:Nom"];
            }
        }

        public static string Source
        {
            get
            {
                return Config["Params:Source"];
            }
        }

        public static int Ecart
        {
            get
            {
                return int.Parse(Config["Params:Ecart"]);
            }
        }
    }
}