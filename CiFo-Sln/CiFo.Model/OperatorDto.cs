using System.Text.Json.Serialization;

namespace Cifo.Model
{
    public class OperatorDto
    {
        [JsonPropertyName("operatorId")]
        public int? operatorId { get; set; }
        [JsonPropertyName("operatorName")]
        public string? operatorName { get; set; }
    }
}
