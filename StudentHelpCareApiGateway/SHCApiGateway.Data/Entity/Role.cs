using Microsoft.AspNetCore.Identity;

namespace SHCApiGateway.Data.Entity
{
    public class Role : IdentityRole
    {
        public string RoleType { get; set; } = string.Empty;
    }
}
