using System.Security.Cryptography;


namespace ConsoleApp1
{
    public static class RsaEncryptor
    {
        public static byte[] Encrypt(byte[] payload, string publicKeyPem)
        {
            using (RSA publicKey = LoadPublicKey(publicKeyPem))
            {
                return publicKey.Encrypt(payload, RSAEncryptionPadding.OaepSHA256);
            }
        }

        private static RSA LoadPublicKey(string publicKeyPem)
        {
            byte[] publicKeyBytes = ParsePem(publicKeyPem, "PUBLIC KEY");

            RSA publicKey = RSA.Create();
            publicKey.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);
            return publicKey;
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
