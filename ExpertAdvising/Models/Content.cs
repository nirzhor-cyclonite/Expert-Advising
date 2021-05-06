using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace trial7.Models
{
    public class Content
    {
        [DisplayName("Content Id")]
        public decimal ContentId { get; set; }
        [DisplayName("Expert Id")]
        public String ExpertId { get; set; }
        [DisplayName("Topic")]
        public String Topic { get; set; }
        [DisplayName("Item")]
        public String Item { get; set; }
        [DisplayName("Content Type")]
        public String ContentType { get; set; }
        [DisplayName("Upload Date")]
        public DateTime UploadDate { get; set; }

    }
}