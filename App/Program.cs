using System;
using System.Collections.Generic;
using System.Linq;

using DAL;
using DAL.Contracts;
using DAL.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var infoBip = new InfoBipSendSmsService(AppConfig.Config());
            foreach (var item in Service.GetEcheances())
            {
                var notification = item.ToSMS();

                if (infoBip.Send(notification))
                {
                    Console.WriteLine($"{ notification.Consentement.Tel} Message- {notification.Message}");
                }
            }

            Console.ReadKey();
        }
    }
}