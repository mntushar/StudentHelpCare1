using Microsoft.AspNetCore.Identity;

namespace StudentHelpCareIdentity.Data.Entity
{
    public class UserEntity : IdentityUser
    {
        public string Discriminator { get; set; } = string.Empty;
    }
}
