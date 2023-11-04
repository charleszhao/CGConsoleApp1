using System.Security.Cryptography;

namespace ConsoleApp1
{
    public static class RsaDecryptor
    {
        public static byte[] Decrypt(byte[] encryptedPayload, string privateKeyPem, string password)
        {
            using (RSA privateKey = LoadPrivateKey(privateKeyPem, password))
            {
                return privateKey.Decrypt(encryptedPayload, RSAEncryptionPadding.OaepSHA256);
            }
        }

        private static RSA LoadPrivateKey(string privateKeyPem, string password)
        {
            byte[] privateKeyBytes = ParsePem(privateKeyPem, "PRIVATE KEY");

            RSA privateKey = RSA.Create();
            privateKey.ImportEncryptedPkcs8PrivateKey(password, privateKeyBytes, out _);
            return privateKey;
        }

        private static byte[] ParsePem(string pem, string section)
        {
            string header = $"-----BEGIN {section}-----";
            string footer = $"-----END {section}-----";

            int startIndex = pem.IndexOf(header) + header.Length;
            int endIndex = pem.IndexOf(footer, startIndex);

            string base64 = pem.Substring(startIndex, endIndex - startIndex);
            return Convert.FromBase64String(base64);
        }
    }
}
