using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifo.Service.Storage
{
    public interface IStorageService
    {
        Task<string> Upload(int userId, Stream file, string contentType, string fileName);
    }
}
