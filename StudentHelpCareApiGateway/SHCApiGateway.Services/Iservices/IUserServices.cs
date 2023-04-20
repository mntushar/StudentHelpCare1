using SHCApiGateway.Data.Model;
using SHCApiGateway.ViewModel.User;
using SHCApiGateway.ViewModel.UserRole;

namespace SHCApiGateway.Services.Iservices
{
    public interface IUserServices
    {
        Task<string> CreateUser(UserViewModel user);
        Task<string> CreateRole(RoleModel role);
    }
}
