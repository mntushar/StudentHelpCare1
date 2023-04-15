using SHCApiGateway.Data.Entity;

namespace SHCApiGateway.ViewModel.UserRole
{
    public class RoleDto
    {
        public static Role Map(
            RoleViewModel viewModel)
        {
            if (viewModel == null)
            {
                return
                    new Role();
            }

            return new Role()
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                NormalizedName = viewModel.NormalizedName,
            };
        }

        public static RoleViewModel Map(
            Role dataEntity)
        {
            if (dataEntity == null) { return new RoleViewModel(); }

            return new RoleViewModel()
            {
                Id = dataEntity.Id,
                Name = dataEntity.Name,
                NormalizedName = dataEntity.NormalizedName,
            };
        }

        public static IEnumerable<RoleViewModel> Map(IEnumerable<Role> dataEntityList)
        {
            if (dataEntityList == null) { yield break; }

            foreach (var item in dataEntityList)
            {
                yield return Map(item);
            }
        }
    }
}
