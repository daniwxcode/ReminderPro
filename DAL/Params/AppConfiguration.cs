using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class AppConfiguration
    {
        public AppConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            AppConfigs.ConnectionString = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            var Param = root.GetSection("Params");
            AppConfigs.Source = Param.GetSection("Source").Value;
            AppConfigs.Ecart = Convert.ToInt32(Param.GetSection("Ecart").Value);
            AppConfigs.Nom = Param.GetSection("Nom").Value;
            AppConfigs.Signature = Param.GetSection("Signature").Value;
            AppConfigs.Sms = Param.GetSection("Sms").Value;
        }
    }
}