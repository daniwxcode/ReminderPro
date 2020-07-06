using System;
using System.Collections.Generic;
using System.Text;

using DAL.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class ReminderContext : DbContext
    {
        private IConfiguration _configuration;

        public ReminderContext()
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(AppConfig.Config()["ConnectionStrings:DataConnection"]);
            optionsBuilder.UseLazyLoadingProxies(false);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        //entities

        public DbSet<Consentement> Consentements { get; set; }
        public DbSet<Echeance> Echeances { get; set; }
    }
}