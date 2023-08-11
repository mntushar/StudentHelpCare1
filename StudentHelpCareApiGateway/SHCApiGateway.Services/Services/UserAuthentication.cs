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
        private ICryptography<User> _cryptography;

        public UserAuthentication(UserManager<User> userManager, SignInManager<User> singInManger,
            ILogger<UserAuthentication> logger, ICryptography<User> cryptography)
        {
            _userManager = userManager;
            _singInManager = singInManger;
            _logger = logger;
            _cryptography = cryptography;
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

                    string Token = _cryptography.OpenIdJwtToken(user.Id, 
                        user.UserName!, user.Email!, roleList, ClaimTypes);

                    string RefreshToken = _cryptography.GenerateToken(user.Id, "UserRefreshToken", 
                        user.SecurityStamp ?? "", ApiGatewayInformation.RefreshTokenValideTime);

                    if (!string.IsNullOrEmpty(Token) && !string.IsNullOrEmpty(RefreshToken))
                    {
                        success.Success = true;
                        success.Token = Token;
                        success.RefreshToken = RefreshToken;
                    }    
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

            return success;
        }

        public async Task<SuccessResult> UserRefreshToken(string token)
        {
            SuccessResult success = await _cryptography.ValidateTokenAsync(token, "UserRefreshToken");

            if (success.Success)
            {
                User? user = await _userManager.FindByIdAsync(success.Message!);

                if (user == null)
                    return success;

                IList<string> roleList = await _userManager.GetRolesAsync(user);
                IList<System.Security.Claims.Claim> ClaimTypes = await _userManager.GetClaimsAsync(user);

                success.Message = _cryptography.OpenIdJwtToken(user.Id, user.UserName!, user.Email!,
                    roleList, ClaimTypes);
            }

            return success;
        }
    }
}
