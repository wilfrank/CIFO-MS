using AuthMicroservice.Models;

namespace AuthMicroservice.Services.Interfaces
{
    public interface IAuthService
    {
        void SignUp(SignUpModel signUpModel);
        Task<LoginModel> Login(string email, string password);
    }
}
