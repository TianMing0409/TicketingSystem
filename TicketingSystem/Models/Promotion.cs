using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketingSystem.Models
{
    public class Promotion
    {
        [Key]
        [ScaffoldColumn(false)]
        public int PromotionId { get; set; }

        public string PromotionTitle { get; set; }

        [DisplayName("Promotion Duration")]
        public string PromotionDuration { get; set; }

        public string PromotionDescription { get; set; }

        public byte[] PromotionPic { get; set; }
    }
}