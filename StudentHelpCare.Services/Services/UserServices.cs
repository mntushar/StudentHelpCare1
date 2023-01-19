using StudentHelpCare.Repository.IRepository;
using StudentHelpCare.Services.IServices;
using StudentHelpCare.ViewModel.User;

namespace StudentHelpCare.Services.Services
{
    public class UserServices : IUserServices
    {
        private IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserViewModel> GetItem(string userName)
        {
            return UserDto.Map(await _userRepository.GetItem(userName));
        }
    }
}
