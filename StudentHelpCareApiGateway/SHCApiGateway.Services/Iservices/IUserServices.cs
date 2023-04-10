using StudentHelpCareIdentity.ViewModel.User;

namespace StudentHelpCareIdentity.Services.Iservices
{
    public interface IUserServices
    {
        Task<string> CreateUser(UserViewModel user);
    }
}
