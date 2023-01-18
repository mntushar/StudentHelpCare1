using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using StudentHelpCare.Services.IServices;
using StudentHelpCare.ViewModel.User;

namespace StudentHelpCare.Services.Services
{
    public class UserRegistrationServices : IUserRegistrationServices
    {
        private UserManager<IdentityUser> _userManager;
        private ILogger<UserRegistrationServices> _logger;

        public UserRegistrationServices(UserManager<IdentityUser> userManager, ILogger<UserRegistrationServices> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<bool> CreateUser(UserViewModel user)
        {
            bool isSuccess = false;

            try
            {
                var userData = new IdentityUser
                {
                    UserName = user.Email,
                    Email = user.Email,
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0
                };

                var result = await _userManager.CreateAsync(userData, user.Password);

                if (result.Succeeded)
                {
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error for create user", ex);
            }

            return isSuccess;
        }
    }
}
