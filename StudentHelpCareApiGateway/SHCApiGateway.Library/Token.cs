using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SHCApiGateway.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SHCApiGateway.Library
{
    public static class Token
    {
        private static readonly ILogger _Logger =
        LoggerFactory.Create(builder =>
            builder.AddConsole())
            .CreateLogger(typeof(Token));
        private static readonly RSAParameters _rsakeyParams = new RSAParameters
        {
            Modulus = Encoding.ASCII.GetBytes("my-custom-key-modulus"),
            Exponent = Encoding.ASCII.GetBytes("my-custom-key-exponent"),
            D = Encoding.ASCII.GetBytes("my-custom-key-d"),
            DP = Encoding.ASCII.GetBytes("my-custom-key-dp"),
            DQ = Encoding.ASCII.GetBytes("my-custom-key-dq"),
            InverseQ = Encoding.ASCII.GetBytes("my-custom-key-inverseq"),
            P = Encoding.ASCII.GetBytes("my-custom-key-p"),
            Q = Encoding.ASCII.GetBytes("my-custom-key-q")
        };
        private static readonly int _rsaKeySize = 2048;
        private static readonly string _SymmetricSecretKry = "test1test";

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

        public static RSAParameters? AsymmetricPrivateKey()
        {
            RSAParameters? privateKey = null;

            try
            {
                using (RSA rsa = RSA.Create(_rsaKeySize))
                {
                    rsa.ImportParameters(_rsakeyParams);

                    // Get the public and private keys
                    privateKey = rsa.ExportParameters(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return privateKey;
        }

        public static RSAParameters? AsymmetricPublicKey()
        {
            RSAParameters? publicKey = null;

            try
            {
                using (RSA rsa = RSA.Create(_rsaKeySize))
                {
                    rsa.ImportParameters(_rsakeyParams);

                    // Get the public and private keys
                    publicKey = rsa.ExportParameters(false);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return publicKey;
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
                _Logger.LogError(ex, "Token Error", ex);
            }

            return tokenString;
        }

        public static string GenerateJWTAsymmetricToken(Claim[] claims,
           RSAParameters privateKey, RSAParameters? publicKey, DateTime tokenValidationTime,
           string algorithom, string issuer, string audience)
        {
            string tokenString = string.Empty;

            try
            {
                // Create signing credentials using the key
                var signingCredentials = new SigningCredentials(new RsaSecurityKey(privateKey), SecurityAlgorithms.RsaSha256);

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
            catch(Exception ex)
            {
                _Logger.LogError(ex, "Token Error", ex);
            }

            return token;
        }
    }
}