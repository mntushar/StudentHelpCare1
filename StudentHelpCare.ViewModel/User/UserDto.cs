namespace StudentHelpCare.ViewModel.User
{
    public class UserDto
    {
        public static Data.Entity.UserEntity Map(UserViewModel viewModel)
        {
            if (viewModel == null) { return new Data.Entity.UserEntity(); }

            return new Data.Entity.UserEntity()
            {
                Id = viewModel.Id,
                UserName = viewModel.UserName,
                Email = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber,
            };
        }

        public static UserViewModel Map(Data.Entity.UserEntity dataEntity)
        {
            if (dataEntity == null) { return new UserViewModel(); }

            return new UserViewModel()
            {
                Id = dataEntity.Id,
                UserName = dataEntity.UserName,
                Email = dataEntity.Email,
                PhoneNumber = dataEntity.PhoneNumber,
            };
        }

        public static IEnumerable<UserViewModel> Map(IEnumerable<Data.Entity.UserEntity> dataEntityList)
        {
            if (dataEntityList == null) { yield break; }

            foreach (var item in dataEntityList)
            {
                yield return Map(item);
            }
        }
    }
}
