using Google.Cloud.Firestore;
namespace Cifo.Model
{
    [FirestoreData]
    public class DocumentDto
    {
        [FirestoreProperty]
        public string? Name { get; set; }
        [FirestoreProperty]
        public string? Label { get; set; }
        [FirestoreProperty]
        public string? Url { get; set; }
        [FirestoreProperty]
        public bool? IsVerified { get; set; }
    }
}
