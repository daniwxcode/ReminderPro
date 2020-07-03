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
                Console.WriteLine($"{i++} - {item.ToSMS()}");
            }

            Console.ReadKey();
        }
    }
}