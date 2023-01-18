using Microsoft.AspNetCore.Identity;
using StudentHelpCare.Services.IServices;
using StudentHelpCare.ViewModel.User;

namespace StudentHelpCare.Services.Services
{
    public class UserRegistrationServices : IUserRegistrationServices
    {
        private UserManager<IdentityUser> _userManager;

        public UserRegistrationServices(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> CreateUser(UserViewModel user)
        {
            bool isSuccess = false;
            var userData = new IdentityUser { UserName = user.Email, Email = user.Email };

            var result = await _userManager.CreateAsync(userData, user.Password);

            if (result.Succeeded)
            {
                isSuccess = true;
            }
            
            return isSuccess;
        }
    }
}
