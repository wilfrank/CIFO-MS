using Cifo.Model;
using Cifo.Service.Interfaces;
using Cifo.Service.Repository;
using Google.Cloud.Firestore;
using Microsoft.Azure.Documents;

namespace Cifo.Service
{
    public class UserService : FirebaseDbRepository<UserModel>, IUserService
    {
        private readonly FirestoreDb db;
        private readonly IFirebaseAuthService firebaseAuthSvc;
        public UserService(FirestoreDb _db, IFirebaseAuthService _firebaseAuthService) : base(_db)
        {
            db = _db;
            firebaseAuthSvc = _firebaseAuthService;
        }
        public async Task<UserModel> CreateAsync(UserModel user)
        {
            //throw new NotImplementedException();
            var docRef = db.Collection(user.GetType().Name).Document(user.Id.ToString());
            await docRef.CreateAsync(user).ConfigureAwait(false);
            return user;
        }

        public void Delete(UserModel user)
        {
            throw new NotImplementedException();
        }

        public async Task<UserModel> GetById(string id)
        {
            var data = db.Collection(new UserModel().GetType().Name).Document(id.ToString());
            var query = await data.GetSnapshotAsync();
            var user = query.ConvertTo<UserModel>();
            return user;
        }
    }
}
