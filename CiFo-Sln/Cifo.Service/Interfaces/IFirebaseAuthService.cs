using Cifo.Model;
using CiFo.Model;
using Firebase.Auth;

namespace Cifo.Service.Interfaces
{
    public interface IFirebaseAuthService
    {
        Task<User?> SignUp(UserModel user);

        Task<FirebaseAuthLink?> Login(SignUpModel user);

        void SingOut();
    }
}
