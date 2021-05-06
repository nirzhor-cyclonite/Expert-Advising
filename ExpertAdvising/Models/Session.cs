using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trial7.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public String UserId { get; set; }
        public String ExpertId { get; set; }
        public String CompletionStatus { get; set; }
        public String Topic { get; set; }

    }
}