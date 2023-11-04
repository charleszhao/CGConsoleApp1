using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp1
{
    public static class TestHelper
    {
        public static byte[] DecryptPayload(string certificateString, byte[] payload)
        {
            byte[] certificateBytes = ParsePem(certificateString);
            var certificate = new X509Certificate2(certificateBytes);
            var publicKey = certificate.GetRSAPublicKey();
            var publicKeyPem = publicKey.ExportSubjectPublicKeyInfo();

            using (RSA key = RSA.Create(2048))
            {
                key.ImportSubjectPublicKeyInfo(publicKeyPem, out _);
                return key.Decrypt(payload, RSAEncryptionPadding.OaepSHA256);
            }

        }

        private static byte[] ParsePem(string pem)
        {
            string header = $"-----BEGIN CERTIFICATE-----";
            string footer = $"-----END CERTIFICATE-----";

            int startIndex = pem.IndexOf(header) + header.Length;
            int endIndex = pem.IndexOf(footer, startIndex);

            string base64 = pem.Substring(startIndex, endIndex - startIndex);
            return Convert.FromBase64String(base64);
        }
    }
}
