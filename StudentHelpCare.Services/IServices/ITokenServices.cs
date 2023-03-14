using Microsoft.AspNetCore.Identity;

namespace StudentHelpCare.StudentHelpCare.Services.IServices
{
    public interface ITokenServices
    {
        Task<string> GenerateTocken(IdentityUser user);
    }
}
