using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trial7.Models
{
    public class CancelledSession
    {
        public decimal SessionId { get; set; }
        public String CancelledFrom { get; set; }
        public String UserType { get; set; }

    }
}