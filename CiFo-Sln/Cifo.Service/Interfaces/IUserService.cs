using Cifo.Model;
using Cifo.Service.Repository;

namespace Cifo.Service.Interfaces
{
    public interface IUserService : IRepository<UserModel>
    {
        Task<UserModel> CreateAsync(UserModel user);
        Task<UserModel> GetById(string id);
        void Delete(UserModel user);
    }
}
