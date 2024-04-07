using Microsoft.AspNetCore.Identity;

namespace Control_Estoque.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Cpf { get; set; }
        public long PhoneNumber { get; set; }
        public string FullName { get; set; }

    }
}
