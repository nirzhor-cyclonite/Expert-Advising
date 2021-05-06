using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trial7.Models
{
    public class SessionDetails
    {
        public int SessionId { get; set; }
        public decimal DurationInMinutes { get; set; }
        public decimal Cost { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public String Brief_Description { get; set; }
        public string Date { get; set;}

    }
}