using System.Text;

namespace SHCApiGateway.Library
{
    public class ApiGatewayInformation
    {
        private static readonly string _jwtSymmetricTokenKry = "test1test";
        public static readonly string url = "https://localhost:5266";

        public static string JwtSymmetricTokenKey()
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(_jwtSymmetricTokenKry);
            string binaryString = Convert.ToString(BitConverter.ToInt64(byteArray, 0), 2).PadLeft(128, '0');

            return binaryString;
        }
    }
}
