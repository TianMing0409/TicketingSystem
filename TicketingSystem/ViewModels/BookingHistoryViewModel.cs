using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketingSystem.Models;

namespace TicketingSystem.ViewModels
{
    public class BookingHistoryViewModel
    {
        public List<Booking> Bookings { get; set; }
    }
}