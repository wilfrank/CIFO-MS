using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIFO.Models.Models
{
    public class AuthenticateDocumentModel
    {
        public int? idCitizen { get; set; }
        public string? UrlDocument { get; set; }
        public string? operatorName { get; set; }
       
    }

}
