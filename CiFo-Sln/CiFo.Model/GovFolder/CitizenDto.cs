using System.Text.Json.Serialization;

namespace Cifo.Model.GovFolder
{
    public class CitizenDto : OperatorDto
    {
        [JsonPropertyName("id")]
        public int? id { get; set; }
        [JsonPropertyName("name")]
        public string? name { get; set; }
        [JsonPropertyName("address")]
        public string? address { get; set; }
        [JsonPropertyName("email")]
        public string? email { get; set; }
    }
}
