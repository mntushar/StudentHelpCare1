using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using StudentHelpCare.Data.Model;
using StudentHelpCare.Services.IServices;
using StudentHelpCare.ViewModel.User;

namespace StudentHelpCare.Services.Services
{
    public class UserAccountServices : IUserAccountServices
    {
        private UserManager<IdentityUser> _userManager;
        private ILogger<UserAccountServices> _logger;
        private SignInManager<IdentityUser> _singInManager;
        private ITokenServices _tokenServices;

        public UserAccountServices(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> singInManger, ITokenServices tokenServices, ILogger<UserAccountServices> logger)
        {
            _userManager = userManager;
            _singInManager = singInManger;
            _tokenServices = tokenServices;
            _logger = logger;
        }

        public async Task<string> CreateUser(UserViewModel user)
        {
            string isSuccess = "false";

            try
            {
                var userData = new IdentityUser
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
                    if(result.Errors.Any())
                    {
                        isSuccess = string.Join("", result.Errors.Select(x => x.Description));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error for create user", ex);
            }

            return isSuccess;
        }

        public async Task<string> UserLogin(UserLoginModel userLogin)
        {
            string success = string.Empty;

            try
            {
                var result = await _singInManager.PasswordSignInAsync(userLogin.UserName,
                           userLogin.Password, userLogin.IsRemember, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(userLogin.UserName);

                    if(user != null)
                    {
                        success = await _tokenServices.GenerateTocken(user);
                    }
                }
                else
                {
                    success = "Please enter valid userName or password...";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login error:", ex);
            }


            return success;
        }
    }
}
