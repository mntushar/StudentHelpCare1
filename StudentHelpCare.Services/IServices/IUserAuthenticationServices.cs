using StudentHelpCare.Data.Model;

namespace StudentHelpCare.Services.IServices
{
    public interface IUserAuthenticationServices
    {
        Task<string> UserLogin(UserLoginModel userLogin);
    }
}
