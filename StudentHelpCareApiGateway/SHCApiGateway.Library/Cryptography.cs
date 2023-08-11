using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.DataProtection;
using SHCApiGateway.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SHCApiGateway.Library
{
    public class Cryptography : ICryptography
    {
        private IDataProtector _protector;

        public Cryptography(IDataProtectionProvider dataProtectionProvider)
        {
            _protector = dataProtectionProvider.Create("Cryptography protection");
        }

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

        public string GenerateToken(string userId, string purpose, string securityStamp, DateTime validityTime)
        {
            string token = string.Empty;

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(purpose) || string.IsNullOrEmpty(securityStamp))
                return token;

            //ArgumentNullException.ThrowIfNull();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(memoryStream))
                {
                    writer.Write(validityTime);
                    writer.Write(userId);
                    writer.Write(purpose);
                    writer.Write(securityStamp);
                }

                // The data is now written to the MemoryStream

                // Reset the position to the beginning of the MemoryStream
                memoryStream.Seek(0, SeekOrigin.Begin);

                var protectedBytes = _protector.Protect(memoryStream.ToArray());
                token = Convert.ToBase64String(protectedBytes);
            }
            //var ms = new MemoryStream();
            ////var userId = await manager.GetUserIdAsync(user);
            //using (var writer = ms.CreateWriter())
            //{
            //    writer.Write(validityTime);
            //    writer.Write(userId);
            //    writer.Write(purpose);
            //    //string? stamp = null;
            //    //if (manager.SupportsUserSecurityStamp)
            //    //{
            //    //    stamp = await manager.GetSecurityStampAsync(user);
            //    //}
            //    writer.Write(securityStamp);
            //}
            //var protectedBytes = _protector.Protect(memoryStream.ToArray());

            return token;
        }
    }
}