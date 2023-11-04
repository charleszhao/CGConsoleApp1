using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ConsoleApp1
{
    public static class CertificateHelper
    {
        public static string GetPublicKeyPemFromCertificate(string certificateString)
        {
            byte[] certificateBytes = Encoding.UTF8.GetBytes(certificateString);
            X509Certificate2 certificate = new X509Certificate2(certificateBytes);

            RSA publicKey = certificate.GetRSAPublicKey();
            var publicKeyPem = publicKey.ExportSubjectPublicKeyInfo();

            return Convert.ToBase64String(publicKeyPem, Base64FormattingOptions.None);
        }
    }
}
