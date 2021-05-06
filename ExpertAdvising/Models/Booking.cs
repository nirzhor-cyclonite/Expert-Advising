using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace trial7.Models
{
    public class Booking
    {
        [DisplayName("Date")]
        public DateTime date { get; set; }

        [DisplayName("Starting Time")]
        public DateTime startTime { get; set; }

        [DisplayName("*Select Hour")]
        [Required(ErrorMessage = "Hour Must Be Selected")]
        public int selectedHour { get; set; }
     
        [DisplayName("*Duration (minutes)")]
        [Required(ErrorMessage = "Duration Must Be Selected")]
        public int durationInMinutes { get; set; }

        [DisplayName("*Topic")]
        [Required(ErrorMessage = "Topic Must Be Given")]
        public string topic { get; set; }

        [DisplayName("Brief Description")]
        public string briefDescription { get; set; }
    }
}