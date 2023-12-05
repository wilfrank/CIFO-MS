using Cifo.Model;
using Cifo.Service.Interfaces;
using CiFo.Model;
using Firebase.Auth;

namespace Cifo.Service
{
    public class FirebaseAuthService : IFirebaseAuthService
    {
        private readonly FirebaseAuthProvider _provider;
        public FirebaseAuthService(string apiKey)
        {
            //_firebaseAuth = firebaseAuth;
            _provider = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
        }

        public async Task<User?> SignUp(UserModel user)
        {
            var userCredentials = await _provider.CreateUserWithEmailAndPasswordAsync(user.Email, user.Password, user.UserName, true);
            return userCredentials is null ? null : userCredentials.User;
        }

        public async Task<FirebaseAuthLink?> Login(SignUpModel user)
        {
            var userCredentials = await _provider.SignInWithEmailAndPasswordAsync(user.Email, user.Password);
            return userCredentials == null ? null : userCredentials;
        }

        public void SingOut()
        {
            //_provider.Dispose();
        }
    }
}