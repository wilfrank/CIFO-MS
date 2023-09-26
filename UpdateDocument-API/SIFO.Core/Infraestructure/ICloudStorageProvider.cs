using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIFO.Core.Infraestructure
{
    public interface ICloudStorageProvider
    {
       Task<string> Upload(int userId,Stream file, string contentType, string fileName);
       
    }
}
