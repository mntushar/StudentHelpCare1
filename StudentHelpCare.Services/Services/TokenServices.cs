using Microsoft.IdentityModel.Tokens;
using StudentHelpCare.Data.Entity;
using StudentHelpCare.Library;
using StudentHelpCare.Services.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace StudentHelpCare.Services.Services
{
    public class TokenServices : ITokenServices
    {
        public async Task<string> GenerateTocken(UserEntity user)
        {
            string finalTocken = string.Empty;

            try
            {
                // reading the content of a private key PEM file, PKCS8 encoded 
                string privateKeyPem = AppInformation.GetAppSecretKey();

                // keeping only the payload of the key 
                privateKeyPem = privateKeyPem.Replace("-----BEGIN PRIVATE KEY-----", "");
                privateKeyPem = privateKeyPem.Replace("-----END PRIVATE KEY-----", "");

                byte[] privateKeyRaw = Convert.FromBase64String(privateKeyPem);

                // creating the RSA key 
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                provider.ImportPkcs8PrivateKey(new ReadOnlySpan<byte>(privateKeyRaw), out _);
                RsaSecurityKey rsaSecurityKey = new RsaSecurityKey(provider);

                // Generating the token 
                var now = DateTime.UtcNow;

                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var handler = new JwtSecurityTokenHandler();

                var token = new JwtSecurityToken
                (
                    user.Id,
                    AppInformation.GetAppUrl(),
                    claims,
                    now.AddMilliseconds(-30),
                    now.AddMinutes(60),
                    new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256)
                );

                finalTocken = handler.WriteToken(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(
                     new System.Diagnostics.StackTrace().ToString()
                );
            }

            return await Task.Run(() => finalTocken);
        }
    }
}
