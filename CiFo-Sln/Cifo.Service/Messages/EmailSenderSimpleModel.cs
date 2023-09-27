using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifo.Service.Messages
{
    public class EmailSenderSimpleModel
    {
        public string Subject { get; set; }

        public string ToEmail { get; set; }

        public string EmailText { get; set; }
    }
}
