using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifo.Model.Document
{
    public class FileDataDTO
    {
        public string? ImageString { get; set; }
        public string? ContentType { get; set; }
        public string? Name { get; set; }
        public int? UserId { get; set; }
    }
}
