using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TicketingSystem.Models
{
    public class BusInfo
    {
        [Key]
        public int BusId { get; set; }

        public string BusCompanyName { get; set; }

        public byte[] BusCompanyLogo { get; set; }

        public string BusPlateNo { get; set; }
    }
}