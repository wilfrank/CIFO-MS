using Firebase.Storage;
using Microsoft.Extensions.Configuration;
using Firebase.Auth;

namespace CIFO.Core.Infraestructure
{
    public class FireBStorageProvider : ICloudStorageProvider
    {
        private readonly IConfiguration _configuration;
        private string _bucketName;
        private string _accessKey;
        private string _secretKey;
        private string _email;
        private string _password;
        private string _apiKey;

        public FireBStorageProvider(IConfiguration configuration)
        {
            _configuration = configuration;
            _bucketName = _configuration["Storage:Bucket"];
            _email = _configuration["Storage:Email"];
            _password = _configuration["Storage:Password"];
            _apiKey = _configuration["Storage:ApiKey"];
        }
        public async Task<string> Upload(int userId, Stream file, string contentType, string fileName)
        {
            try
            {
                Guid guid = Guid.NewGuid();

                var auth = new FirebaseAuthProvider(new FirebaseConfig(_apiKey));
                var a = await auth.SignInWithEmailAndPasswordAsync(_email, _password);

                var task = new FirebaseStorage(
                         _bucketName,
                          new FirebaseStorageOptions
                          {
                              AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                              ThrowOnCancel = true,
                          })
                         .Child(userId.ToString())
                         .Child(fileName+'_'+guid)
                         .PutAsync(file);

                task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

                var downloadUrl = await task;
                return downloadUrl;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al subir el archivo: {ex.Message}");
            }

            return "";
        }
    }
}
