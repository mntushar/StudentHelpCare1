using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SHCApiGateway.Data.Model;
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
        private readonly char _stringSeparator = ';';
        private readonly int _tokenEntity = 4;
        private UserManager<Tuser> _userManager;

        public Cryptography(UserManager<Tuser> userManager)
        {
            _userManager = userManager;
        }

        public string ProtectData(string data)
        {
            string protectString = string.Empty;

            try
            {
                var destFolder = Path.Combine(_dataProtectKeyPath, _dataProtectKeyFileName);

                // Instantiate the data protection system at this folder
                var dataProtectionProvider = DataProtectionProvider.Create(
                    new DirectoryInfo(destFolder));

                var protector = dataProtectionProvider.CreateProtector("Cryptography protection");
                protectString = protector.Protect(data);
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

        public string GenerateDefaultSymmetricJwtToken(string userId, string userName, string userEmail,
            IList<string> roleList, IList<System.Security.Claims.Claim> ClaimTypes)
        {
            string token = string.Empty;

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userName)
                || string.IsNullOrEmpty(userEmail))
                return token;

            try
            {
                string role = string.Join(",", roleList.Select(r => r.ToString()));
                string claim = string.Join(",", ClaimTypes.Select(c => c.ToString()));


                var clims = new[]
                {
                    new Claim("Id", userId),
                    new Claim("name", userName),
                    new Claim("email", userEmail),
                    new Claim("role", role),
                    new Claim("claimTypes", claim),
                    new Claim("iat", new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()),
                };

                token = GenerateJWTSymmetricToken(clims, ApiGatewayInformation.SymmetricKey,
                                ApiGatewayInformation.TokenValideTime, SecurityAlgorithms.HmacSha256,
                                ApiGatewayInformation.url, ApiGatewayInformation.url);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return token;
        }

        public string OpenIdJwtToken(string userId, string userName,
            string userEmail, IList<string> roleList, IList<System.Security.Claims.Claim> ClaimTypes)
        {
            string token = string.Empty;

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userName)
                || string.IsNullOrEmpty(userEmail))
                return token;

            try
            {
                string role = string.Join(",", roleList.Select(r => r.ToString()));
                string claim = string.Join(",", ClaimTypes.Select(c => c.ToString()));


                var clims = new[]
                {
                    new Claim("Id", userId),
                    new Claim("name", userName),
                    new Claim("email", userEmail),
                    new Claim("role", role),
                    new Claim("claimTypes", claim),
                    new Claim("iat", new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()),
                };

                token = GenerateJWTAsymmetricToken(clims, DateTime.Now.AddDays(1),
                    ApiGatewayInformation.url,
                    ApiGatewayInformation.url);
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

            string[] listInfo = new string[_tokenEntity];
            listInfo[0] = userId;
            listInfo[1] = purpose;
            listInfo[2] = securityStamp;
            listInfo[3] = validityTime.ToString();
            string info = string.Join(_stringSeparator, listInfo);

            token = ProtectData(info);

            return token;
        }

        public async Task<SuccessResult> ValidateTokenAsync(string token, string purpose)
        {
            SuccessResult success = new SuccessResult();

            try
            {
                var unprotectedData = UnProtectData(token);

                string[] listInfo = unprotectedData.Split(_stringSeparator);

                if (!listInfo.Any() || listInfo.Count() < _tokenEntity)
                    return success;

                DateTime validityDate = DateTime.Parse(listInfo[3]);
                if(validityDate < DateTime.Now)
                {
                    Console.WriteLine("InvalidExpirationTime");
                    return success;
                }

                string userId = listInfo[0];
                Tuser? user = await _userManager.FindByIdAsync(userId);
                if(user == null)
                {
                    Console.WriteLine("UserIdsNotEquals");
                    return success;
                }

                string userPrpose = listInfo[1];
                if(!string.Equals(userPrpose, purpose))
                {
                    Console.WriteLine("PurposeNotEquals");
                    return success;
                }

                string userSecurityStamp = listInfo[2];
                if (_userManager.SupportsUserSecurityStamp)
                {
                    string securityStamp = await _userManager.GetSecurityStampAsync(user);
                    if (!string.Equals(userSecurityStamp, securityStamp))
                    {
                        Console.WriteLine("PurposeNotEquals");
                        return success;
                    }
                }

                success.Success = true;
                success.Message = userId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return success;
        }
    }
}