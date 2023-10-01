using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIFO.Models.Models
{
    public class AuthenticateDocumentCompleteModel
    {
       public AuthenticateDocumentModel AuthenticateModel { get; set; }
       public DocumentModel DocumentModel { get; set; } 
       
    }

}
