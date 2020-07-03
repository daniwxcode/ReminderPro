using System;
using System.Collections.Generic;
using System.Linq;

using DAL;

using Microsoft.EntityFrameworkCore;

namespace App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int i = 1;
            foreach (var item in Service.GetEcheances(360))
            {
                var notification = item.ToSMS();
                Console.WriteLine($"{ notification.Consentement.Tel} Message- {notification.Message}");
            }

            Console.ReadKey();
        }
    }
}