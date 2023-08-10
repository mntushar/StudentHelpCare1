using Microsoft.IdentityModel.Tokens;
using SHCApiGateway.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SHCApiGateway.Library
{
    public static class Cryptography
    {
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
                throw new ArgumentNullException(ex.Message);
            }

            return tokenString;
        }

        public static string GenerateJWTAsymmetricToken(Claim[] claims,
           DateTime tokenValidationTime, string issuer, string audience)
        {
            string tokenString = string.Empty;

            try
            {
                if (!File.Exists(ApiGatewayInformation.AsyJwtCertification))
                {
                    return tokenString;
                }

                // Create signing credentials using the key
                var certificate = new X509Certificate2(ApiGatewayInformation.AsyJwtCertification,
                    ApiGatewayInformation.AsyJwtPrivateKeyDecryptPassword);

                var signingCredentials = new SigningCredentials(new X509SecurityKey(certificate),
                    SecurityAlgorithms.RsaSha256);

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
                throw new ArgumentNullException(ex.Message);
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
                    new Claim("Id", user.Id),
                    new Claim("name", user.UserName!),
                    new Claim("email", user.Email!),
                    new Claim("role", role),
                    new Claim("claimTypes", claim),
                    new Claim("iat", new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()),
                };

                    token = GenerateJWTSymmetricToken(clims, ApiGatewayInformation.SymmetricKey,
                                    ApiGatewayInformation.TokenValideTime, SecurityAlgorithms.HmacSha256,
                                    ApiGatewayInformation.url, ApiGatewayInformation.url);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(ex.Message);
            }

            return token;
        }

        public static string OpenIdJwtToken(User user, IList<string> roleList,
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
                    new Claim("Id", user.Id),
                    new Claim("name", user.UserName!),
                    new Claim("email", user.Email!),
                    new Claim("role", role),
                    new Claim("claimTypes", claim),
                    new Claim("iat", new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()),
                };

                    token = GenerateJWTAsymmetricToken(clims, DateTime.Now.AddDays(1),
                        ApiGatewayInformation.url,
                        ApiGatewayInformation.url);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(ex.Message);
            }

            return token;
        }
    }
}