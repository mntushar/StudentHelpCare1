using Microsoft.AspNetCore.Identity;

namespace StudentHelpCareIdentity.ViewModel.User
{
    public class UserDto
    {
        public static IdentityUser Map(UserViewModel viewModel)
        {
            if (viewModel == null) { return new IdentityUser(); }

            return new IdentityUser()
            {
                Id = viewModel.Id,
                UserName = viewModel.UserName,
                Email = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber,
            };
        }

        public static UserViewModel Map(IdentityUser dataEntity)
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

        public static IEnumerable<UserViewModel> Map(IEnumerable<IdentityUser> dataEntityList)
        {
            if (dataEntityList == null) { yield break; }

            foreach (var item in dataEntityList)
            {
                yield return Map(item);
            }
        }
    }
}
