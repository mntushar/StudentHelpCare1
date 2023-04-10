using StudentHelpCareIdentity.ViewModel.User;

namespace StudentHelpCareIdentity.Services.Iservices
{
    public interface IRegisterUserServices
    {
        Task<string> CreateUser(UserViewModel user);
    }
}
