using System.Security.Claims;

namespace SHCApiGateway.Library
{
    public interface ICryptography<Tuser> where Tuser : class
    {
        string ProtectData(byte[] data);
        string UnProtectData(string data);
        string GenerateJWTSymmetricToken(Claim[] claims,
            string secretKey, DateTime tokenValidationTime,
            string algorithom, string issuer, string audience);
        string GenerateJWTAsymmetricToken(Claim[] claims,
           DateTime tokenValidationTime, string issuer, string audience);
        string GenerateDefaultSymmetricJwtToken(string userId, string userName, string userEmail,
            IList<string> roleList, IList<System.Security.Claims.Claim> ClaimTypes);
        string OpenIdJwtToken(string userId, string userName,
            string userEmail, IList<string> roleList, IList<System.Security.Claims.Claim> ClaimTypes);
        string GenerateToken(string userId, string purpose, string securityStamp, DateTime validityTime);
        Task<bool> ValidateTokenAsync(string token, string purpose);
    }
}
