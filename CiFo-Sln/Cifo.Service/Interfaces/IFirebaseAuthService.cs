using Cifo.Model;
namespace Cifo.Service.Interfaces
{
    public interface IFirebaseAuthService
    {
        Task<string?> SignUp(UserModel user);

        Task<string?> Login(UserModel user);

        void SingOut();
    }
}
