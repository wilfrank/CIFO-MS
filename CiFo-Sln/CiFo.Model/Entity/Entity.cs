using Google.Cloud.Firestore;
using Newtonsoft.Json;

namespace Cifo.Model.Entity
{
    public abstract class Entity
    {
        [JsonProperty(PropertyName = "Key")]
        [FirestoreProperty(Name = "Key")]
        public string Id { get; set; }
    }
}
