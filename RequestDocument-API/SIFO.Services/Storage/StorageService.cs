using CIFO.Core.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIFO.Services.Storage
{
    public class StorageService : IStorageService
    {
        private readonly ICloudStorageProvider _cloudStorageProvider;

        public StorageService(ICloudStorageProvider cloudStorageProvider)
        {
            _cloudStorageProvider = cloudStorageProvider;
        }
        public Task<string> Upload(int userId, Stream file, string contentType, string fileName)
        {
            return _cloudStorageProvider.Upload(userId,file, contentType,fileName);
        }
    }
}
