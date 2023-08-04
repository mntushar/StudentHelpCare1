namespace SHCApiGateway.Library
{
    public class ApiGatewayInformation
    {
        public static readonly string url = "https://localhost:5266";
        public static readonly string TokenSymmetricSecretKry = "test1test";
        public static readonly string AsyJwtPrivateKeyDecryptPassword = "test";

        private static string CertificationPath
        {
            get
            {
                string path = Directory.GetCurrentDirectory();
                int lastIndex = path.LastIndexOf("\\");
                path = path.Remove(lastIndex + 1);
                path = $"{path}SHCApiGateway.Library\\Certifications\\";

                return path;
            }
        }

        public static string AsyJwtCertification
        {
            get
            {
                string path = $"{CertificationPath}asy_jwt_certificate.pfx";

                return path;
            }
        }
    }
}
