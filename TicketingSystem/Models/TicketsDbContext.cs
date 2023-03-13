using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TicketingSystem.Models
{
    public class TicketsDbContext : DbContext
    {
        public TicketsDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<BusTrip> BusTrips { get; set; }

        public DbSet<OriginPlace> OriginPlaces { get; set; }

        public DbSet<DestinationPlace> DestinationPlaces { get; set; }

        public DbSet<BusInfo> BusInfos { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Promotion> Promotions { get; set; }

        //public System.Data.Entity.DbSet<TicketingSystem.ViewModels.BusTripViewModel> BusTripViewModels { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>()
                .HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId });

            modelBuilder.Entity<IdentityUserRole>()
                .HasKey(r => new { r.UserId, r.RoleId });

            base.OnModelCreating(modelBuilder);
        }
    }
}