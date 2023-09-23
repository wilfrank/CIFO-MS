using AuthMicroservice.Models;
using AuthMicroservice.Services.Interfaces;
using Firebase.Auth;

namespace AuthMicroservice.Services
{
    public class AuthService : IAuthService
    {
        private readonly FirebaseAuthProvider _provider;
        public AuthService(string apiKey)
        {
            _provider = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
        }
        public async Task<LoginModel> Login(string email, string password)
        {
            var loginModel = new LoginModel();
            try
            {
                var validateLogIn = await _provider.SignInWithEmailAndPasswordAsync(email, password);
                if (validateLogIn.User != null)
                {
                    loginModel = new LoginModel
                    {
                        Email = validateLogIn.User.Email,
                        Id = validateLogIn.User.LocalId,
                        Name = validateLogIn.User.DisplayName
                    };
                }
            }
            catch (Exception ex)
            {
                loginModel = new LoginModel() { Error = ex.Message };
            }
            return loginModel;
        }

        public void SignUp(SignUpModel signUpModel)
        {
           _provider.CreateUserWithEmailAndPasswordAsync(signUpModel.Email, signUpModel.Password, signUpModel.Name, true);
        }
    }
}
