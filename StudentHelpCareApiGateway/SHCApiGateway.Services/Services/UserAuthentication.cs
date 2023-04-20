using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SHCApiGateway.Data.Entity;
using SHCApiGateway.Library;
using SHCApiGateway.Services.Iservices;
using StudentHelpCare.Identity.Data.Model;

namespace SHCApiGateway.Services.Services
{
    public class UserAuthentication : IUserAuthentication
    {
        private UserManager<User> _userManager;
        private ILogger<UserAuthentication> _logger;
        private SignInManager<User> _singInManager;

        public UserAuthentication(UserManager<User> userManager, SignInManager<User> singInManger,
            ILogger<UserAuthentication> logger)
        {
            _userManager = userManager;
            _singInManager = singInManger;
            _logger = logger;
        }

        public async Task<string> UserLogin(UserLoginModel userLogin)
        {
            string success = string.Empty;

            try
            {
                if (userLogin == null || string.IsNullOrEmpty(userLogin.UserName) 
                    || string.IsNullOrEmpty(userLogin.Password)) return success;

                var result = await _singInManager.PasswordSignInAsync(userLogin.UserName,
                           userLogin.Password, userLogin.IsRemember, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    User? user = await _userManager.FindByNameAsync(userLogin.UserName);

                    if (user != null)
                    {
                        IList<string> roleList = await _userManager.GetRolesAsync(user);
                        IList<System.Security.Claims.Claim> ClaimTypes = await _userManager.GetClaimsAsync(user);

                        //Generate Symmetric Jwt Token
                        success = Token.GenerateDefaultSymmetricJwtToken(user, roleList, ClaimTypes);
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
