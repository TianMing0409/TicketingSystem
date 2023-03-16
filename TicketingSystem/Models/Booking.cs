using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Booking ID")]
        public int BookingId { get; set; }

        [ScaffoldColumn(false)]
        public int BusTripId { get; set; }

        [ScaffoldColumn(false)]
        public string UserId { get; set; }

        public DateTime BookingDate { get; set; }

        [Required]
        [DisplayName("Passenger Name")]
        public string PassengerName { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter a valid phone number.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must have 10 digits. Cannot have '-'. ")]
        public string PhoneNo { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [DisplayName("Depart Fare")]
        public decimal Total { get; set; }

        public virtual BusTrip BusTrip { get; set; }

    }
}