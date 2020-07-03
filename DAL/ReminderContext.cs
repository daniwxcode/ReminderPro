using System;
using System.Collections.Generic;
using System.Text;

using DAL.Model;

using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ReminderContext : DbContext
    {
        public ReminderContext()
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var App = new AppConfiguration();
            optionsBuilder.UseSqlServer(AppConfigs.ConnectionString);
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