using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIFO.Models.Models
{
    public class DocumentModel
    {
        public int? IdUser { get; set; }
        public string? DocumentName { get; set; }
        public string? Status { get; set; }

    }

}
