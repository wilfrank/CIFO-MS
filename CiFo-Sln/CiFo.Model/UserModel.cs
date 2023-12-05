using CiFo.Model;
using Google.Cloud.Firestore;

namespace Cifo.Model
{
    [FirestoreData]
    public class UserModel : SignUpModel
    {
        [FirestoreProperty]
        public string? FirstName { get; set; }
        [FirestoreProperty]
        public string? LastName { get; set; }
        [FirestoreProperty]
        public string? IdentityType { get; set; }
        [FirestoreProperty]
        public string? IdentityNumber { get; set; }
        [FirestoreProperty]
        public string? PhoneNumber { get; set; }
        [FirestoreProperty]
        public string? Address { get; set; }
        [FirestoreProperty]
        public bool IsActived { get; set; }
        [FirestoreProperty]
        public List<DocumentDto>? Documents { get; set; }
    }
}
