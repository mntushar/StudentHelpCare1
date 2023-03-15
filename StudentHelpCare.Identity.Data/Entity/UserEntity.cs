using Microsoft.AspNetCore.Identity;

namespace StudentHelpCare.Identity.Data.Entity
{
    public class UserEntity : IdentityUser
    {
        public string Discriminator { get; set; } = string.Empty;
    }
}
