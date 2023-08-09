using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SHCApiGateway.Library
{
    public class ApiGatewayInformation
    {
        public static readonly string url = "https://localhost:5266";
        private static readonly string _tokenSymmetricSecretKry = "test1test";
        public static readonly string AsyJwtPrivateKeyDecryptPassword = "test";
        public static readonly DateTime TokenValideTime = DateTime.Now.Date.AddDays(1);


        private static string CertificationPath
        {
            get
            {
                string path = string.Empty;
                try
                {
                    path = Directory.GetCurrentDirectory();
                    int lastIndex = path.LastIndexOf("\\");
                    path = path.Remove(lastIndex + 1);
                    path = $"{path}SHCApiGateway.Library\\Certifications\\";
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

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

        public static RsaSecurityKey? AsyJwtECDsaPublicKey
        {
            get
            {
                // Load the X509Certificate2 from a file or store
                X509Certificate2 certificate = new X509Certificate2(AsyJwtCertification, 
                    AsyJwtPrivateKeyDecryptPassword);

                // Get the RSA private key from the certificate
                RSA? publicKey = certificate.GetRSAPublicKey();

                if (publicKey == null)
                {
                    return null;
                }

                // Create an instance of RsaSecurityKey
                RsaSecurityKey rsaPublicKey = new RsaSecurityKey(publicKey);

                return rsaPublicKey;
            }
        }

        public static string SymmetricKey
        {
            get
            {
                string key = string.Empty;

                try
                {
                    byte[]? byteArray = Encoding.UTF8.GetBytes(_tokenSymmetricSecretKry);

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
        }
    }
}
