using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace eBoxOffice.Domain.Domain_models
{
    public class EmailMessage : BaseEntity
    {
        public string MailTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool Status { get; set; }
    }

}