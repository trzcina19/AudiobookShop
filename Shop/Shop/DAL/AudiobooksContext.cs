using Microsoft.AspNet.Identity.EntityFramework;
using Shop.Controllers;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Shop.DAL
{
    public class AudiobooksContext: IdentityDbContext<ApplicationUser>
    {
      
        public AudiobooksContext() : base("AudiobooksContext")
        {

        }

        // Initialization of basic data
        static AudiobooksContext()
        {
            Database.SetInitializer<AudiobooksContext>(new AudiobooksInitializer());
        }


        public static AudiobooksContext Create()
        {
            return new AudiobooksContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Disables a convention that automatically generates a plural for table names in a database
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Audiobook> Audiobooks { get; set; } 
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PositionOrder> PositionsOrder { get; set; }
    }
}