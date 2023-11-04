using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp1.OcbcPayNow
{
    public class RequestHeaderPayload
    {
        public string Authorization { get; private set; }
        public string Timestamp { get; private set; }
        public string Resource { get; private set; }
        public string Payload { get; private set; }

        private string StringToSign
        {
            get
            {
                return $"{nameof(Authorization)}:{Authorization};{nameof(Timestamp)}:{Timestamp};{nameof(Resource)}:{Resource};{Payload}";
            }
        }

        public RequestHeaderPayload(string authorization, string timestamp, string resource, string payload)
        {
            Authorization = authorization;
            Timestamp = timestamp;
            Resource = resource;
            Payload = payload;
        }

        public string Sign(string privateKey)
        {
            byte[] dataByte = Encoding.UTF8.GetBytes(StringToSign);

            using (RSA rsa = RSA.Create())
            {
                rsa.ImportFromPem(privateKey);

                // Generate a digital signature using JTC private key
                byte[] signatureByte = rsa.SignData(dataByte, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                return Convert.ToBase64String(signatureByte);
            }
        }

        public bool Verify(string signature, string publicKey)
        {
            byte[] dataByte = Encoding.UTF8.GetBytes(StringToSign);
            byte[] signatureByte = Convert.FromBase64String(signature);

            using (RSA rsa = RSA.Create())
            {
                rsa.ImportFromPem(publicKey);

                // Verify the signature using OCBC public key
                bool isSignatureValid = rsa.VerifyData(dataByte, signatureByte, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                return isSignatureValid;
            }
        }
    }
}
