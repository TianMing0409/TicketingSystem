using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketingSystem.Models
{
    public class BusTrip
    {
        public int BusTripId { get; set; }

        public int BusId { get; set; }

        [Required]
        [DisplayName("Origin")]
        public int OriginPlaceId { get; set; }

        [Required]
        [DisplayName("Destination")]
        public int DestinationPlaceId { get; set; }

        [Required]
        [DisplayName("Departure Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DepartureDate { get; set; }

        [DisplayName("Return Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ReturnDate { get; set; }

        [Required]
        [DisplayName("Departure Time")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public string DepartureTime { get; set; }

        [Required]
        [DisplayName("Seat Available(s)")]
        public int SeatAvailable { get; set; }

        [Required]
        [DisplayName("Price (RM)")]
        public decimal Price { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:N1}", ApplyFormatInEditMode = true)]
        [DisplayName("Rating")]
        public decimal Rating { get; set; }

        public virtual OriginPlace OriginPlace { get; set; }

        public virtual DestinationPlace DestinationPlace { get; set; }

        public virtual BusInfo BusInfos { get; set; }

    }
}