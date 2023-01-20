using Microsoft.AspNetCore.Identity;
using StudentHelpCare.Data.Entity;

namespace StudentHelpCare.Services.IServices
{
    public interface ITokenServices
    {
        Task<string> GenerateTocken(IdentityUser user);
    }
}
