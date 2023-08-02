using Microsoft.IdentityModel.Tokens;
using SHCApiGateway.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SHCApiGateway.Library
{
    public static class Cryptography
    {
        private static readonly int _rsaKeySize = 2048;
        private static readonly string _SymmetricSecretKry = "test1test";


        public static string AsymmtricKeyPath
        {
            get
            {
                string path = Directory.GetCurrentDirectory();
                int lastIndex = path.LastIndexOf("\\");
                path = path.Remove(lastIndex + 1);
                path = path + "SHCApiGateway.Library\\Files";

                return path;
            }
        }

        public static string SymmetricKey()
        {
            string key = string.Empty;

            try
            {
                byte[]? byteArray = Encoding.UTF8.GetBytes(_SymmetricSecretKry);

                if (byteArray == null) return key;

                using (var hmac = new HMACSHA256(byteArray))
                {
                    byte[] byteKey = hmac.ComputeHash(byteArray);
                    byte[] truncatedKey = new byte[16];
                    Array.Copy(byteKey, truncatedKey, 16);

                    key = Convert.ToBase64String(truncatedKey);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return key;
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
                Console.WriteLine(ex);
            }

            return tokenString;
        }

        public static string GenerateJWTAsymmetricToken(Claim[] claims,
           DateTime tokenValidationTime, string algorithom, string issuer, string audience)
        {
            string tokenString = string.Empty;

            try
            {
                // Create signing credentials using the key
                using var rsa = new RSACryptoServiceProvider(_rsaKeySize);
                RSAParameters keyParams = rsa.ExportParameters(true);
                var signingCredentials = new SigningCredentials(new RsaSecurityKey(keyParams), SecurityAlgorithms.RsaSha256);

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
                Console.WriteLine(ex);
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

                    token = GenerateJWTSymmetricToken(clims, SymmetricKey(),
                                    DateTime.Now.AddDays(1), SecurityAlgorithms.HmacSha256, ApiGatewayInformation.url,
                                    ApiGatewayInformation.url);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
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
                        SecurityAlgorithms.HmacSha256, ApiGatewayInformation.url, 
                        ApiGatewayInformation.url);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return token;
        }
    }
}