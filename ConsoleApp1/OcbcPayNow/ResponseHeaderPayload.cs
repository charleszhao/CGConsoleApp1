using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp1.OcbcPayNow
{
    public class ResponseHeaderPayload
    {
        public string Timestamp { get; private set; }
        public string Payload { get; private set; }

        private string StringToSign
        {
            get
            {
                return $"{nameof(Timestamp)}:{Timestamp};{Payload}";
            }
        }

        public ResponseHeaderPayload(string timestamp, string payload)
        {
            Timestamp = timestamp;
            Payload = payload;
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
