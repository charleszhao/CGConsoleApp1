using System.Buffers.Text;
using System.Security.Cryptography;

namespace ConsoleApp1.OcbcPayNow
{
    public static class Utility
    {
        public static RSA LoadPrivateKey(string privateKeyContent, string password)
        {
            byte[] privateKeyBytes = ParsePem(privateKeyContent, "PRIVATE KEY");

            RSA privateKey = RSA.Create();
            privateKey.ImportEncryptedPkcs8PrivateKey(password, privateKeyBytes, out _);
            return privateKey;
        }

        public static RSA LoadPublicKey(string publicKeyContent)
        {
            byte[] publicKeyBytes = ParsePem(publicKeyContent, "PUBLIC KEY");

            RSA publicKey = RSA.Create();
            publicKey.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);
            return publicKey;
        }

        public static byte[] ParsePem(string pem, string section)
        {
            string header = $"-----BEGIN {section}-----";
            string footer = $"-----END {section}-----";

            int startIndex = pem.IndexOf(header) + header.Length;
            int endIndex = pem.IndexOf(footer, startIndex);

            string base64 = pem.Substring(startIndex, endIndex - startIndex).Trim();
            return Convert.FromBase64String(base64);
        }
    }
}
