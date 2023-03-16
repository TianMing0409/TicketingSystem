using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DisplayName("Total")]
        public decimal TotalAmount { get; set; }

        [DisplayName("Processing Fee")]
        public decimal ProcessingFee { get; set; }

        public decimal Discount { get; set; }

        [DisplayName("Promo Code")]
        public string PromoCode { get; set; }

        public bool IsPromoCodeValid { get; set; }

    }
}