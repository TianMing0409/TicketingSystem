using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketingSystem.Models
{
    public class OriginPlace
    {
        [Key]
        public int OriginPlaceId { get; set; }

        [Required]
        [DisplayName("Origin")]
        public string OriginPlaceName { get; set; }


        [Required]
        [DisplayName("State")]
        public string OriginState { get; set; }
    }
}