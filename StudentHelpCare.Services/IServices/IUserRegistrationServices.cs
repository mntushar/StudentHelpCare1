using StudentHelpCare.ViewModel.User;

namespace StudentHelpCare.Services.IServices
{
    public interface IUserRegistrationServices
    {
        Task<bool> CreateUser(UserViewModel user);
    }
}
