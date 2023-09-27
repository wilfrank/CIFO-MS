using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifo.Model.Document
{
    public class AuthenticateDocumentModel
    {
        public int? idCitizen { get; set; }
        public string? UrlDocument { get; set; }
        public string? documentTitle { get; set; }

    }
}
