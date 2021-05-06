using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace trial7.Models
{
    public class Calendar
    {
        public int id { get; set; }
        public String ExpertId { get; set; }
        [DisplayName("Starting From")]
        public DateTime StartingFrom { get; set; }
        [DisplayName("Ending At")]
        [Range(0,24)]
        public DateTime EndingAt { get; set; }
        [DisplayName("Starting Hour")]
        [Range(1, 24)]
        public int startingHour { get; set; }
        [DisplayName("Ending Hour")]
        public int endingHour { get; set; }
        public string BookingDate { get; set; }

    }
}