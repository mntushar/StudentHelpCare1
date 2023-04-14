using StudentHelpCare.Identity.Data.Model;

namespace SHCApiGateway.Services.Iservices
{
    public interface IUserAuthentication
    {
        Task<string> UserLogin(UserLoginModel userLogin);
    }
}
