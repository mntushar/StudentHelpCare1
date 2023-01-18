using Microsoft.AspNetCore.Identity;

namespace StudentHelpCare.Data.Entity
{
    public class UserEntity : IdentityUser
    {
        public string Discriminator { get; set; } = null!;
    }
}
