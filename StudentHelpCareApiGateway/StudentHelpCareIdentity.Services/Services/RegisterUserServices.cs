using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using StudentHelpCareIdentity.Data.Entity;
using StudentHelpCareIdentity.Services.Iservices;
using StudentHelpCareIdentity.ViewModel.User;

namespace StudentHelpCareIdentity.Services.Services
{
    public class RegisterUserServices : IRegisterUserServices
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ILogger<RegisterUserServices> _logger;

        public RegisterUserServices(
        UserManager<UserEntity> userManager,
        ILogger<RegisterUserServices> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }


        public async Task<string> CreateUser(UserViewModel user)
        {
            string isSuccess = "false";

            try
            {
                if (user != null)
                {
                    var userData = new UserEntity
                    {
                        UserName = user.UserName,
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
                        isSuccess = "true";
                    }
                    else
                    {
                        if (result.Errors.Any())
                        {
                            isSuccess = string.Join("", result.Errors.Select(x => x.Description));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login error:", ex);
            }

            return isSuccess;
        }
    }
}
