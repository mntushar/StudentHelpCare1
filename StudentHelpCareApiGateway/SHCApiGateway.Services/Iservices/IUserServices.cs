using SHCApiGateway.Data.Model;
using SHCApiGateway.ViewModel.User;

namespace SHCApiGateway.Services.Iservices
{
    public interface IUserServices
    {
        Task<SuccessResult> CreateUser(UserViewModel user);
        Task<SuccessResult> CreateRole(RoleModel role);
    }
}
