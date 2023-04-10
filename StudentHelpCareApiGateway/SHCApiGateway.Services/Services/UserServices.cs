using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SHCApiGateway.Data.Entity;
using SHCApiGateway.Services.Iservices;
using SHCApiGateway.ViewModel.User;

namespace SHCApiGateway.Services.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UserServices> _logger;

        public UserServices(
        UserManager<User> userManager,
        ILogger<UserServices> logger)
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
                    var userData = new User
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
