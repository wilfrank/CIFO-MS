using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifo.Service.Storage
{
    public interface IStorageService
    {
        Task<string> Upload(Int64 userId, Stream file, string contentType, string fileName);
    }
}
