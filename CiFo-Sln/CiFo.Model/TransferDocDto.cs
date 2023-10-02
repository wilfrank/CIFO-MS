namespace Cifo.Model
{
    public class TransferDocDto
    {
        public int Id { get; set; }
        public string? CitizenName { get; set; }
        public string? CitizenEmail { get; set; }
        public Dictionary<string,string>? UrlDocuments { get; set; }
    }
}
