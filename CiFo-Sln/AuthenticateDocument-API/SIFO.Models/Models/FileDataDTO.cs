using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIFO.Models.Models
{
    public class FileDataDTO
    {
        public string? ImageString { get; set; }
        public string? ContentType { get; set; }
        public string? Name { get; set; }
        public string? DocumentRouting { get; set; }
        public int? UserId { get; set; }
        public string? UserEmail { get; set; }
    }

}
