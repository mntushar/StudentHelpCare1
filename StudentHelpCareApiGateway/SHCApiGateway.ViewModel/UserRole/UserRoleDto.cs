namespace SHCApiGateway.ViewModel.UserRole
{
    public class UserRoleDto
    {
        public static SHCApiGateway.Data.Entity.UserRole Map(
            UserRoleViewModel viewModel)
        {
            if (viewModel == null)
            {
                return
                    new SHCApiGateway.Data.Entity.UserRole();
            }

            return new SHCApiGateway.Data.Entity.UserRole()
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                NormalizedName = viewModel.NormalizedName,
            };
        }

        public static UserRoleViewModel Map(
            SHCApiGateway.Data.Entity.UserRole dataEntity)
        {
            if (dataEntity == null) { return new UserRoleViewModel(); }

            return new UserRoleViewModel()
            {
                Id = dataEntity.Id,
                Name = dataEntity.Name,
                NormalizedName = dataEntity.NormalizedName,
            };
        }

        public static IEnumerable<UserRoleViewModel> Map(IEnumerable<SHCApiGateway.Data.Entity.UserRole> dataEntityList)
        {
            if (dataEntityList == null) { yield break; }

            foreach (var item in dataEntityList)
            {
                yield return Map(item);
            }
        }
    }
}
