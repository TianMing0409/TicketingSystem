using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketingSystem.Models
{
    public class DestinationPlace
    {
        [Key]
        public int DestinationPlaceId { get; set; }

        [Required]
        [DisplayName("Destination")]
        public string DestinationPlaceName { get; set; }


        [Required]
        [DisplayName("State")]
        public string DestinationState { get; set; }

    }
}