using StudentHelpCare.ViewModel.User;

namespace StudentHelpCare.Services.IServices
{
    public interface IUserRegistrationServices
    {
        Task<string> CreateUser(UserViewModel user);
    }
}
