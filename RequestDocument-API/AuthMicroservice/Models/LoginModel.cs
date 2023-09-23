namespace AuthMicroservice.Models
{
    public class LoginModel
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Error { get; set; }
    }
}
