namespace SHCApiGateway.ViewModel.UserClim
{
    public class UserClimDto
    {
        public static SHCApiGateway.Data.Entity.UserClim Map(
            UserClimViewModel viewModel)
        {
            if (viewModel == null)
            {
                return
                    new SHCApiGateway.Data.Entity.UserClim();
            }

            return new SHCApiGateway.Data.Entity.UserClim()
            {
                Id = viewModel.Id,
                UserId = viewModel.UserId,
                ClaimType = viewModel.ClaimType,
                ClaimValue = viewModel.ClaimValue,
            };
        }

        public static UserClimViewModel Map(
            SHCApiGateway.Data.Entity.UserClim dataEntity)
        {
            if (dataEntity == null) { return new UserClimViewModel(); }

            return new UserClimViewModel()
            {
                Id = dataEntity.Id,
                UserId = dataEntity.UserId,
                ClaimType = dataEntity.ClaimType,
                ClaimValue = dataEntity.ClaimValue,
            };
        }

        public static IEnumerable<UserClimViewModel> Map(IEnumerable<SHCApiGateway.Data.Entity.UserClim> dataEntityList)
        {
            if (dataEntityList == null) { yield break; }

            foreach (var item in dataEntityList)
            {
                yield return Map(item);
            }
        }
    }
}
