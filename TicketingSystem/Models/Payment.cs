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
        public string Id { get; set; }

        [ScaffoldColumn(false)]
        public decimal PaymentAmount { get; set; }

        [ScaffoldColumn(false)]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "Please enter the cardholder name.")]
        [DisplayName("Name*")]
        public string CardHolderName { get; set; }

        [Required(ErrorMessage = "Please enter the card number.(without '-' or space)")]
        [RegularExpression(@"\d{16}", ErrorMessage = "Please enter a valid 16-digit card number.")]
        [DisplayName("Card number*")]
        public string CardNo { get; set; }

        [Required(ErrorMessage = "Please enter the expiration date.")]
        [RegularExpression(@"(0[1-9]|1[0-2])/[0-9]{2}", ErrorMessage = "Please enter a valid expiration date in the format MM/YY.")]
        [DisplayName("Expiration Date*")]
        public string ExpirationDate { get; set; }

        [Required(ErrorMessage = "Please enter the CVC.")]
        [RegularExpression(@"\d{3}", ErrorMessage = "Please enter a valid 3-digit CVC code.")]
        [DisplayName("CVC*")]
        public int? Cvc { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}