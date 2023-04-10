using Microsoft.IdentityModel.Tokens;
using SHCApiGateway.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SHCApiGateway.Library
{
    public static class Token
    {
        public static string CreateJWTSymmetricToken(User user, string secretKey)
        {
            // Define your symmetric key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            // Create signing credentials using the key
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256);

            // Create some claims to include in the token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            // Define the token's options
            var tokenOptions = new JwtSecurityToken(
                issuer: "your_issuer_here",
                audience: "your_audience_here",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signingCredentials
            );

            // Generate the token as a string
            string tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }
    }
}