using Microsoft.AspNetCore.Identity;

namespace SHCApiGateway.ViewModel.UserRoleClaim
{
    public class UserRoleClaimDto
    {
        public static IdentityRoleClaim<string> Map(
           UserRoleClaimViewModel viewModel)
        {
            if (viewModel == null)
            {
                return
                    new IdentityRoleClaim<string>();
            }

            return new IdentityRoleClaim<string>()
            {
                Id = viewModel.Id,
                RoleId = viewModel.RoleId,
                ClaimType = viewModel.ClaimType,
                ClaimValue = viewModel.ClaimValue,
            };
        }

        public static UserRoleClaimViewModel Map(
            IdentityRoleClaim<string> dataEntity)
        {
            if (dataEntity == null) { return new UserRoleClaimViewModel(); }

            return new UserRoleClaimViewModel()
            {
                Id = dataEntity.Id,
                RoleId = dataEntity.RoleId,
                ClaimType = dataEntity.ClaimType,
                ClaimValue = dataEntity.ClaimValue,
            };
        }

        public static IEnumerable<UserRoleClaimViewModel> Map(IEnumerable<IdentityRoleClaim<string>> dataEntityList)
        {
            if (dataEntityList == null) { yield break; }

            foreach (var item in dataEntityList)
            {
                yield return Map(item);
            }
        }
    }
}
