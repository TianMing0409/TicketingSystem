using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TicketingSystem.Models;

namespace TicketingSystem.ViewModels
{
    public class BookingHistoryViewModel
    {
        [DisplayName("Booking ID")]
        public int BookingId { get; set; }
        public int BusTripId { get; set; }

        public string OriginPlace { get; set; }

        public string DestinationPlace { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DepartureDate { get; set; }

        public string DepartureTime { get; set; }

        public string BusCompanyName { get; set; }

        public byte[] BusCompanyLogo { get; set; }

        [DisplayFormat(DataFormatString = "{0:N1}", ApplyFormatInEditMode = true)]
        [DisplayName("Rating")]
        public decimal Rating { get; set; }

        public string UserId { get; set; }

        [DisplayName("Booking Date")]
        public DateTime BookingDate { get; set; }
        public string PassengerName { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public decimal Total { get; set; }

        [DisplayName("Payment Amount")]
        public decimal PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }

        public virtual BusTrip BusTrip { get; set; }

        public virtual Booking Booking { get; set; }

        public virtual Payment Payment { get; set; }
    }
}