namespace Cifo.Model
{
    public class TransferDocDto
    {
        public int Id { get; set; }
        public string? CitizenName { get; set; }
        public string? CitizenEmail { get; set; }
        public List<string>? UrlDocuments { get; set; }
    }
}
