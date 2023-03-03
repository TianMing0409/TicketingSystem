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
    }
}