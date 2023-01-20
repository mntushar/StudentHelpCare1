using StudentHelpCare.Data.Model;
using StudentHelpCare.ViewModel.User;

namespace StudentHelpCare.Services.IServices
{
    public interface IUserAccountServices
    {
        Task<string> CreateUser(UserViewModel user);
        Task<string> UserLogin(UserLoginModel userLogin);
    }
}
