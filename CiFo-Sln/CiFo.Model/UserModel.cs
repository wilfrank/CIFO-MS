using CiFo.Model;

namespace Cifo.Model
{
    public class UserModel : SignUpModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? IdentityType { get; set; }
        public string? IdentityNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}
