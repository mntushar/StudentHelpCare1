namespace SHCApiGateway.Library
{
    public class ApiGatewayInformation
    {
        public static readonly string url = "https://localhost:5266";
        public static readonly string TokenSymmetricSecretKry = "test1test";

        public static string JwtCertification
        {
            get
            {
                string path = Directory.GetCurrentDirectory();
                int lastIndex = path.LastIndexOf("\\");
                path = path.Remove(lastIndex + 1);
                path = $"{path}SHCApiGateway.Library\\Certifications\\JwtCertification.pem";

                return path;
            }
        }
    }
}
