namespace SHCApiGateway.ViewModel.User
{
    public class UserDto
    {
        public static SHCApiGateway.Data.Entity.User Map(UserViewModel viewModel)
        {
            if (viewModel == null) { return new SHCApiGateway.Data.Entity.User(); }

            return new SHCApiGateway.Data.Entity.User()
            {
                Id = viewModel.Id,
                UserName = viewModel.UserName,
                Email = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber,
            };
        }

        public static UserViewModel Map(SHCApiGateway.Data.Entity.User dataEntity)
        {
            if (dataEntity == null) { return new UserViewModel(); }

            return new UserViewModel()
            {
                Id = dataEntity.Id,
                UserName = dataEntity.UserName!,
                Email = dataEntity.Email!,
                PhoneNumber = dataEntity.PhoneNumber!,
            };
        }

        public static IEnumerable<UserViewModel> Map(IEnumerable<SHCApiGateway.Data.Entity.User> dataEntityList)
        {
            if (dataEntityList == null) { yield break; }

            foreach (var item in dataEntityList)
            {
                yield return Map(item);
            }
        }
    }
}
