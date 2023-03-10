using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TicketingSystem.Models
{
    [Bind(Exclude = "BookingId")]

    public class Booking
    {
        [ScaffoldColumn(false)]
        public int BookingId { get; set; }

        [ScaffoldColumn(false)]
        public int BusTripId { get; set; }

        public DateTime BookingDate { get; set; }

        [Required]
        public string PassengerName { get; set; }

        [Required]
        public string PhoneNo { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        public decimal Total { get; set; }

        public virtual BusTrip BusTrip { get; set; }

    }
}