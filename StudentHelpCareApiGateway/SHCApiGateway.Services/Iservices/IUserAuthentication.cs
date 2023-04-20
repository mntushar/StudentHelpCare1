using SHCApiGateway.Data.Model;
using StudentHelpCare.Identity.Data.Model;

namespace SHCApiGateway.Services.Iservices
{
    public interface IUserAuthentication
    {
        Task<AuthenticationResult> UserLogin(UserLoginModel userLogin);
    }
}
