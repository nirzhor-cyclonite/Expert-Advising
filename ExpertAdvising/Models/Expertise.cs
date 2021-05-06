using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trial7.Models
{
    public class Expertise
    {
        public String ExpertId { get; set; }
        public String Education { get; set; }
        public String WorkingArea { get; set; }
        public String ExpertiseSectors { get; set; }
        public decimal Fee { get; set; }
        public decimal Rating { get; set; }
        public string Bio { get; set; }
    }
}