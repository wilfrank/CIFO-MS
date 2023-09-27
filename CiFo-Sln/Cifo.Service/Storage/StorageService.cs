using CIFO.Core.Infraestructure;

namespace Cifo.Service.Storage
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
            return _cloudStorageProvider.Upload(userId, file, contentType, fileName);
        }
    }
}
