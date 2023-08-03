using System.Security.Cryptography.X509Certificates;
using static System.Formats.Asn1.AsnWriter;

namespace SHCApiGateway.Library
{
    public static class X509Certificates
    {
        private static readonly StoreName storeName = StoreName.My;
        private static readonly StoreLocation storeLocation = StoreLocation.CurrentUser;
        private static X509Certificate2? certificate = null;
        private static readonly X509Store store = new X509Store(storeName, storeLocation);


        public static X509Certificate2? GetCertificite()
        {
            try
            {
                store.Open(OpenFlags.ReadOnly);
                X509Certificate2Collection certificates = store.Certificates.Find(
                    X509FindType.FindByThumbprint,
                    "Personal",
                    validOnly: false // Set this to true if you only want valid (not expired) certificates
                );

                if (certificates.Count > 0)
                {
                    certificate = certificates[0];
                    // You can select the appropriate certificate based on your criteria.
                    // Here, we are using the first certificate found with the given thumbprint.

                    return certificate;
                }
            }
            finally
            {
                store.Close();
            }

            return certificate;
        }
    }
}
