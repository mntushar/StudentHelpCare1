using StudentHelpCare.StudentHelpCare.Data.Model;
using StudentHelpCare.StudentHelpCare.ViewModel.User;

namespace StudentHelpCare.StudentHelpCare.Services.IServices
{
    public interface IUserAccountServices
    {
        Task<string> CreateUser(UserViewModel user);
        Task<string> UserLogin(UserLoginModel userLogin);
    }
}
