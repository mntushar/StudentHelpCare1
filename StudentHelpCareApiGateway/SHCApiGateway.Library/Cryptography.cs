using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SHCApiGateway.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SHCApiGateway.Library
{
    public class Cryptography<Tuser> : ICryptography<Tuser> where Tuser : class
    {
        private readonly string _dataProtectKeyPath = ApiGatewayInformation.CertificationPath;
        private readonly string _dataProtectKeyFileName = "DataProtectionKey";
        private UserManager<Tuser> _userManager;

        public Cryptography(UserManager<Tuser> userManager)
        {
            _userManager = userManager;
        }

        public string ProtectData(byte[] data)
        {
            string protectString = string.Empty;

            try
            {
                var destFolder = Path.Combine(_dataProtectKeyPath, _dataProtectKeyFileName);

                // Instantiate the data protection system at this folder
                var dataProtectionProvider = DataProtectionProvider.Create(
                    new DirectoryInfo(destFolder));

                var protector = dataProtectionProvider.CreateProtector("Cryptography protection");
                byte[] protectedBytes = protector.Protect(data);

                protectString = Convert.ToBase64String(protectedBytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return protectString;
        }

        public string UnProtectData(string data)
        {
            string unprotectString = string.Empty;

            try
            {
                var destFolder = Path.Combine(_dataProtectKeyPath, _dataProtectKeyFileName);

                // Instantiate the data protection system at this folder
                var dataProtectionProvider = DataProtectionProvider.Create(
                    new DirectoryInfo(destFolder));

                var protector = dataProtectionProvider.CreateProtector("Cryptography protection");
                unprotectString = protector.Unprotect(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return unprotectString;
        }

        public string GenerateJWTSymmetricToken(Claim[] claims,
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
                Console.WriteLine(ex.Message);
            }

            return tokenString;
        }

        public string GenerateJWTAsymmetricToken(Claim[] claims,
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
                Console.WriteLine(ex.Message);
            }

            return tokenString;
        }

        public string GenerateDefaultSymmetricJwtToken(Tuser user, IList<string> roleList,
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
                Console.WriteLine(ex.Message);
            }

            return token;
        }

        public string OpenIdJwtToken(Tuser user, IList<string> roleList,
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
                Console.WriteLine(ex.Message);
            }

            return token;
        }

        public string GenerateToken(string userId, string purpose, string securityStamp, DateTime validityTime)
        {
            string token = string.Empty;

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(purpose) || string.IsNullOrEmpty(securityStamp))
                return token;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(memoryStream))
                {
                    writer.Write(validityTime);
                    writer.Write(userId);
                    writer.Write(purpose);
                    writer.Write(securityStamp);
                }

                token = ProtectData(memoryStream.ToArray());
            }

            return token;
        }

        public async Task<bool> ValidateTokenAsync(string token, string purpose, Tuser user)
        {
            try
            {
                var unprotectedData = UnProtectData(token);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (StreamReader reader = new StreamReader(memoryStream))
                    {
                        var creationTime = reader.ReadDateTimeOffset();
                        var expirationTime = creationTime + Options.TokenLifespan;
                        if (expirationTime < DateTimeOffset.UtcNow)
                        {
                            Console.WriteLine("InvalidExpirationTime");
                            return false;
                        }

                        var userId = reader.Read();
                        var actualUserId = await _userManager.GetUserIdAsync(user);
                        if (userId != actualUserId)
                        {
                            Console.WriteLine("UserIdsNotEquals");
                            return false;
                        }

                        var purp = reader.Read();
                        if (!string.Equals(purp, purpose))
                        {
                            Console.WriteLine("PurposeNotEquals");
                            return false;
                        }

                        var stamp = reader.Read();
                        if (reader.PeekChar() != -1)
                        {
                            Console.WriteLine("UnexpectedEndOfInput");
                            return false;
                        }

                        if (_userManager.SupportsUserSecurityStamp)
                        {
                            var isEqualsSecurityStamp = stamp == await _userManager.GetSecurityStampAsync(user);
                            if (!isEqualsSecurityStamp)
                            {
                                Console.WriteLine("SecurityStampNotEquals");
                            }

                            return isEqualsSecurityStamp;
                        }

                        var stampIsEmpty = stamp == "";
                        if (!stampIsEmpty)
                        {
                            Console.WriteLine("SecurityStampIsNotEmpty");
                        }

                        return stampIsEmpty;
                    }
                }

                //var ms = new MemoryStream(unprotectedData);
                //using (var reader = ms.CreateReader())
                //{
                //    var creationTime = reader.ReadDateTimeOffset();
                //    var expirationTime = creationTime + Options.TokenLifespan;
                //    if (expirationTime < DateTimeOffset.UtcNow)
                //    {
                //        Logger.InvalidExpirationTime();
                //        return false;
                //    }

                //    var userId = reader.ReadString();
                //    var actualUserId = await manager.GetUserIdAsync(user);
                //    if (userId != actualUserId)
                //    {
                //        Logger.UserIdsNotEquals();
                //        return false;
                //    }

                //    var purp = reader.ReadString();
                //    if (!string.Equals(purp, purpose))
                //    {
                //        Logger.PurposeNotEquals(purpose, purp);
                //        return false;
                //    }

                //    var stamp = reader.ReadString();
                //    if (reader.PeekChar() != -1)
                //    {
                //        Logger.UnexpectedEndOfInput();
                //        return false;
                //    }

                //    if (manager.SupportsUserSecurityStamp)
                //    {
                //        var isEqualsSecurityStamp = stamp == await manager.GetSecurityStampAsync(user);
                //        if (!isEqualsSecurityStamp)
                //        {
                //            Logger.SecurityStampNotEquals();
                //        }

                //        return isEqualsSecurityStamp;
                //    }

                //    var stampIsEmpty = stamp == "";
                //    if (!stampIsEmpty)
                //    {
                //        Logger.SecurityStampIsNotEmpty();
                //    }

                    //return stampIsEmpty;
                //}
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch(Exception ex)
            {
                // Do not leak exception
                Console.WriteLine(ex.Message);
            }

            return false;
        }
    }
}