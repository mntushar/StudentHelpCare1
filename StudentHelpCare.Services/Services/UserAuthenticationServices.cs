using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using StudentHelpCare.Data.Model;
using StudentHelpCare.Services.IServices;
using StudentHelpCare.ViewModel.User;

namespace StudentHelpCare.Services.Services
{
    public class UserAuthenticationServices : IUserAuthenticationServices
    {
        private ILogger<UserAuthenticationServices> _logger;
        private SignInManager<IdentityUser> _singInManager;
        private ITokenServices _tokenServices;
        private IUserServices _userServices;

        public UserAuthenticationServices(ILogger<UserAuthenticationServices> logger, SignInManager<IdentityUser> singInManger, ITokenServices tokenServices, IUserServices userServices)
        {
            _logger = logger;
            _singInManager = singInManger;
            _tokenServices = tokenServices;
            _userServices = userServices;
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
                    var user = await _userServices.GetItem(userLogin.UserName);
                    var token = _tokenServices.GenerateTocken(UserDto.Map(user));
                }
                else
                {
                    success = "Please enter valid userName or password...";
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Login error:", ex);
            }
            

            return success;
        }
    }
}
