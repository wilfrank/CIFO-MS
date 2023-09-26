using Cifo.Model;
using Cifo.Service.Interfaces;
using Firebase.Auth;

namespace Cifo.Service
{
    public class FirebaseAuthService : IFirebaseAuthService
    {
        //private readonly FirebaseAuthClient _firebaseAuth;
        private readonly FirebaseAuthProvider _provider;
        public FirebaseAuthService(string apiKey)
        {
            //_firebaseAuth = firebaseAuth;
            _provider = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
        }

        public async Task<string?> SignUp(UserModel user)
        {
            var userCredentials = await _provider.CreateUserWithEmailAndPasswordAsync(user.Email, user.Password, user.UserName, true);
            return userCredentials is null ? null : userCredentials.User.LocalId;
        }

        public async Task<string?> Login(UserModel user)
        {
            var userCredentials = await _provider.SignInWithEmailAndPasswordAsync(user.Email, user.Password);
            return userCredentials == null ? null : userCredentials.User.LocalId;
        }

        public void SingOut()
        {
            //_provider.Dispose();
        }
    }
}