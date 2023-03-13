using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketingSystem.Models;

namespace TicketingSystem.ViewModels
{
    public class PaymentViewModel
    {

        public virtual BusTrip BusTrip { get; set; }

        public virtual Booking Booking { get; set; }

        public virtual Payment Payment { get; set; }

    }
}