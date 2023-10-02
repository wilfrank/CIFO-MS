using System.Text.Json.Serialization;

namespace Cifo.Model
{
    public class OperatorCompleteDto
    {
       public OperatorDto Operator { get; set; }
       public string UrlOperatorToChange { get; set; }
    }
}
