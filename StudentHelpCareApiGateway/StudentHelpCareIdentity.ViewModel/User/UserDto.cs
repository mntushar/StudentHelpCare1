using StudentHelpCareIdentity.Data.Entity;

namespace StudentHelpCareIdentity.ViewModel.User
{
    public class UserDto
    {
        public static UserEntity Map(UserViewModel viewModel)
        {
            if (viewModel == null) { return new UserEntity(); }

            return new UserEntity()
            {
                Id = viewModel.Id,
                UserName = viewModel.UserName,
                Email = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber,
            };
        }

        public static UserViewModel Map(UserEntity dataEntity)
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

        public static IEnumerable<UserViewModel> Map(IEnumerable<UserEntity> dataEntityList)
        {
            if (dataEntityList == null) { yield break; }

            foreach (var item in dataEntityList)
            {
                yield return Map(item);
            }
        }
    }
}
