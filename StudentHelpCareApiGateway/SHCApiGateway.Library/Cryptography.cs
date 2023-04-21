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
        private static readonly RSAParameters _rsakeyParams = new RSAParameters
        {
            Modulus = Convert.FromBase64String(
                "WDEikZ7zB1anY42i7DQjbJMNyE9XnsaO+ACaFJ61TjT4g7EN+ypYdOpPqH4GGz4f/vxDXlKItU6Hq3x+GSlgQ=="),
            Exponent = Convert.FromBase64String(
                "WDEikZ7zB1anY42i7DQjbJMNyE9XnsaO+ACaFJ61TjT4g7EN+ypYdOpPqH4GGz4f/vxDXlKItU6Hq3x+GSlgQ=="),
            D = Convert.FromBase64String("WDEikZ7zB1anY42i7DQjbJMNyE9XnsaO+ACaFJ61TjT4g7EN+ypYdOpPqH4GGz4f/vxDXlKItU6Hq3x+GSlgQ=="),
            DP = Convert.FromBase64String("R+zsNOOK9+lpg44cJF5+wv2xWxK5Z5J5nzb+1AaH8W5uGcvoSPy9Vx8WlLh7tBLwEYikZUCzjKQ2fJUKVQGLQ=="),
            DQ = Convert.FromBase64String("AnoywYdZDXs+Gn4J3qjrK6hJUfE1X0rC/f6q+UoJhN0zJ/kNV8fdSTb+AKvpgj1iJ12Hzh+8aQF5mLz5mGh5w=="),
            InverseQ = Convert.FromBase64String(
                "Zaxw0fd80bhsdNfM/sF+xS9yjVi94BWyf3TtTqskt4sZt4sZt4sZt4sZt4sZt4sZt4sZt4sZt4sZt4sZt4sZt4sA=="),
            P = Convert.FromBase64String("3GTR1dmyZ1gKjw4oMIP+kWpwR8rXl/lr27ODbrrFG2YjKm8xalZBc94Zw3qitOce"),
            Q = Convert.FromBase64String("y0fjKk99CXc7V2Z0Mue5dVmqNGSTXYu8y7Gg+lz13u0=")

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

        public static RSAParameters RsaAsymmetricPrivateKey()
        {
            RSAParameters privateKey = new RSAParameters();

            try
            {
                using (RSA rsa = RSA.Create(_rsaKeySize))
                {
                    rsa.ImportParameters(_rsakeyParams);

                    // Get the private keys
                    privateKey = rsa.ExportParameters(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return privateKey;
        }

        public static RSAParameters RsaAsymmetricPublicKey()
        {
            RSAParameters publicKey = new RSAParameters();

            try
            {
                using (RSA rsa = RSA.Create(_rsaKeySize))
                {
                    rsa.ImportParameters(_rsakeyParams);

                    // Get the public keys
                    publicKey = rsa.ExportParameters(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
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
                Console.WriteLine(ex);
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

                    token = GenerateJWTAsymmetricToken(clims, RsaAsymmetricPrivateKey(), RsaAsymmetricPublicKey(),
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
    }
}