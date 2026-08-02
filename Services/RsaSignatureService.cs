using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using YellowphaseWebsite.Models.Configurations;

namespace YellowphaseWebsite.Services
{
    public class RsaSignatureService : IRsaSignatureService
    {
        private readonly ZanacoSettings _settings;

        public RsaSignatureService(
            IOptions<ZanacoSettings> settings)
        {
            _settings = settings.Value;
        }

        public string Sign(string payload)
        {
            var privateKey =
                File.ReadAllText(_settings.PrivateKeyPath);

            using RSA rsa = RSA.Create();

            rsa.ImportFromPem(privateKey);

            byte[] data =
                Encoding.UTF8.GetBytes(payload);

            byte[] signature =
                rsa.SignData(
                    data,
                    HashAlgorithmName.SHA256,
                    RSASignaturePadding.Pkcs1);

            return Convert.ToBase64String(signature);
        }

        public bool Verify(
            string payload,
            string signature)
        {
            var publicKey =
                File.ReadAllText(
                    _settings.ZanacoPublicKeyPath);

            using RSA rsa = RSA.Create();

            rsa.ImportFromPem(publicKey);

            byte[] data =
                Encoding.UTF8.GetBytes(payload);

            byte[] signatureBytes =
                Convert.FromBase64String(signature);

            return rsa.VerifyData(
                data,
                signatureBytes,
                HashAlgorithmName.SHA256,
                RSASignaturePadding.Pkcs1);
        }
    }
}