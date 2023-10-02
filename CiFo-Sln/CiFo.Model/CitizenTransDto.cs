using System.Text.Json.Serialization;

namespace Cifo.Model
{
    public class CitizenTransDto
    {
        public TransferDocDto TransferDocDto { get; set; }
        public string UrlOperatorToChange { get; set; }
    }
}
