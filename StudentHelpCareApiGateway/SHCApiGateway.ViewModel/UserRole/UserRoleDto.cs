using Microsoft.AspNetCore.Identity;

namespace SHCApiGateway.ViewModel.UserRole
{
    public class UserRoleDto
    {
        public static IdentityUserRole<string> Map(
           UserRoleViewModel viewModel)
        {
            if (viewModel == null)
            {
                return
                    new IdentityUserRole<string>();
            }

            return new IdentityUserRole<string>()
            {
                UserId = viewModel.UserId,
                RoleId = viewModel.RoleId,
            };
        }

        public static UserRoleViewModel Map(
            IdentityUserRole<string> dataEntity)
        {
            if (dataEntity == null) { return new UserRoleViewModel(); }

            return new UserRoleViewModel()
            {
                UserId = dataEntity.UserId,
                RoleId = dataEntity.RoleId,
            };
        }

        public static IEnumerable<UserRoleViewModel> Map(IEnumerable<IdentityUserRole<string>> dataEntityList)
        {
            if (dataEntityList == null) { yield break; }

            foreach (var item in dataEntityList)
            {
                yield return Map(item);
            }
        }
    }
}
