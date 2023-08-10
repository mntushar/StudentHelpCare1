using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SHCApiGateway.Data.Entity;
using SHCApiGateway.Data.Model;
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

        public async Task<AuthenticationResult> UserLogin(UserLoginModel userLogin)
        {
            AuthenticationResult success = new AuthenticationResult();

            try
            {
                if (userLogin == null || string.IsNullOrEmpty(userLogin.UserName)
                    || string.IsNullOrEmpty(userLogin.Password)) return success;

                var result = await _singInManager.PasswordSignInAsync(userLogin.UserName,
                           userLogin.Password, userLogin.IsRemember, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    User? user = await _userManager.FindByNameAsync(userLogin.UserName);

                    if (user == null)
                    {
                        return success;
                    }

                    IList<string> roleList = await _userManager.GetRolesAsync(user);
                    IList<System.Security.Claims.Claim> ClaimTypes = await _userManager.GetClaimsAsync(user);

                    //Generate Symmetric Jwt Token
                    success.Token = Cryptography.OpenIdJwtToken(user, roleList, ClaimTypes);

                    success.RefreshToken = "'test";

                    if (!string.IsNullOrEmpty(success.Token) && !string.IsNullOrEmpty(success.RefreshToken))
                        success.Success = true;
                    else
                        success = new AuthenticationResult();
                }
                else
                {
                    success.Message = "Please enter valid userName or password...";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login error:", ex);
            }
            finally 
            { 
                success = new AuthenticationResult();
            }

            return success;
        }
    }
}
