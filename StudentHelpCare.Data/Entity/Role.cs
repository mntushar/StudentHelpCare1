using Microsoft.AspNetCore.Identity;

namespace StudentHelpCare.Data.Entity
{
    public class Role : IdentityRole
    {
        public bool IsRead { get; set; }
        public bool IsWrite { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
    }
}
