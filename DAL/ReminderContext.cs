using System;
using System.Collections.Generic;
using System.Text;
using DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ReminderContext : DbContext
    {
        public ReminderContext ()
        {
            Database.Migrate();
        }

        protected override void OnConfiguring ( DbContextOptionsBuilder optionsBuilder )
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-SKIHT4L;Database=RemindDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating ( ModelBuilder modelBuilder )
        {
        }
        //entities
        public DbSet<Api> Apis { get; set; }
        public DbSet<Engagement> Engagements { get; set; }
        public DbSet<Configs> Configs{get;set;}
    }
}
