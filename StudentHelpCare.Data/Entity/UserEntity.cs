using Microsoft.AspNetCore.Identity;

namespace StudentHelpCare.StudentHelpCare.Data.Entity
{
    public class UserEntity : IdentityUser
    {
        public string Discriminator { get; set; } = null!;
    }
}
