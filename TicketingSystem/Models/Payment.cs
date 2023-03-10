using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TicketingSystem.Models
{
    public class Payment
    {
        [Key]
        [ScaffoldColumn(false)]
        public int PaymentId { get; set; }

        [ScaffoldColumn(false)]
        public int BookingId { get; set; }

        [ScaffoldColumn(false)]
        public decimal PaymentAmount { get; set; }

        [ScaffoldColumn(false)]
        public DateTime PaymentDate { get; set; }

        [Required]
        [DisplayName("Name*")]
        public string CardHolderName { get; set; }

        [Required]
        [DisplayName("Card number*")]
        public string CardNo { get; set; }

        [Required]
        [DisplayName("Expiration Date*")]
        public string ExpirationDate { get; set; }

        [Required]
        [DisplayName("CVC*")]
        public int? Cvc { get; set; }
    }
}