using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SHCApiGateway.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SHCApiGateway.Library
{
    public static class Token
    {
        private static readonly ILogger _Logger =
        LoggerFactory.Create(builder =>
            builder.AddConsole())
            .CreateLogger(typeof(Token));

        public static string GenerateJWTSymmetricToken(Claim[] claims,
            string secretKey, DateTime tokenValidationTime,
            string algorithom, string issuer, string audience)
        {
            string tokenString = string.Empty;

            try
            {
                // Define your symmetric key
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

                // Create signing credentials using the key
                var signingCredentials = new SigningCredentials(securityKey, algorithom);

                // Define the token's options
                var tokenOptions = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: tokenValidationTime,
                    signingCredentials: signingCredentials
                );

                // Generate the token as a string
                tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Token Error", ex);
            }

            return tokenString;
        }

        public static string GenerateDefaultSymmetricJwtToken(User user, IList<string> roleList,
            IList<System.Security.Claims.Claim> ClaimTypes)
        {
            string token = string.Empty;

            try
            {
                string role = string.Join(",", roleList.Select(r => r.ToString()));
                string claim = string.Join(",", ClaimTypes.Select(c => c.ToString()));

                if (user != null)
                {
                    var clims = new[]
                    {
                    new Claim("claim-type", user.Id),
                    new Claim("name", user.UserName!),
                    new Claim("email", user.Email!),
                    new Claim("role", role),
                    new Claim("claimTypes", claim),
                    new Claim("iat", new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()),
                };

                    token = GenerateJWTSymmetricToken(clims, ApiGatewayInformation.jwtSymmetricTokenKry,
                                 DateTime.Now.AddDays(1), SecurityAlgorithms.HmacSha256, ApiGatewayInformation.url,
                                 ApiGatewayInformation.url);
                }
            }
            catch(Exception ex)
            {
                _Logger.LogError(ex, "Token Error", ex);
            }

            return token;
        }
    }
}