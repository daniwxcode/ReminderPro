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
            var smsService = new InfoBipSendSmsService(AppConfig.Config());
            var mailService = new SendGridEmailSenderService(AppConfig.Config());
            foreach (var item in Service.GetEcheances())
            {
                //  var notification = item.ToSMS();
                var mail = item.ToMail();
                mailService.SendMail(mail);
                //if (smsService.Send(notification).Result)
                //{
                //    Console.WriteLine($"{ notification.Consentement.Tel} Message- {notification.Message}");
                //}
            }

            Console.ReadKey();
        }
    }
}