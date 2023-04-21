using System.Security.Cryptography;
using System.Text;

namespace SHCApiGateway.Library
{
    public class ApiGatewayInformation
    {
        private static readonly string _jwtSymmetricTokenKry = "test1test";
        public static readonly string url = "https://localhost:5266";

        public static string JwtSymmetricTokenKey()
        {
            string key = string.Empty;

            byte[]? byteArray = Encoding.UTF8.GetBytes(_jwtSymmetricTokenKry);

            if (byteArray == null) return key;

            using (var hmac = new HMACSHA256(byteArray))
            {
                byte[] byteKey = hmac.ComputeHash(byteArray);
                byte[] truncatedKey = new byte[16];
                Array.Copy(byteKey, truncatedKey, 16);

                key = Convert.ToBase64String(truncatedKey);
            }

            return key;
        }
    }
}
