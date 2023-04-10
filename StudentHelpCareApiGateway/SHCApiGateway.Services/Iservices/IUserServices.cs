using SHCApiGateway.ViewModel.User;

namespace SHCApiGateway.Services.Iservices
{
    public interface IUserServices
    {
        Task<string> CreateUser(UserViewModel user);
    }
}
