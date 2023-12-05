using Cifo.Model.Entity;
using Google.Cloud.Firestore;

namespace CiFo.Model
{
    public class SignUpModel : Entity
    {
        [FirestoreProperty]
        public string? UserName { get; set; }
        [FirestoreProperty]
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}