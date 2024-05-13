using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServerAPI.Server.Core.Encryption
{
    public class Encryption
    {
        public Encryption()
        {
            SaltBytes = GenerateRandomCryptographicBytes();
        }

        public byte[] SaltBytes;

        readonly int SaltLength = 64;

        public string GenerateRandomCryptographicKey()
        {
            return Convert.ToBase64String(GenerateRandomCryptographicBytes());
        }

        public byte[] GenerateRandomCryptographicBytes()
        {
            RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[SaltLength];
            rngCryptoServiceProvider.GetBytes(randomBytes);

            return randomBytes;
        }

        public HashWithSaltResult HashWithSalt(string target, HashAlgorithm hashAlgo)
        {
            byte[] targetAsBytes = Encoding.UTF8.GetBytes(target);
            List<byte> targetWithSaltBytes = new List<byte>();
            targetWithSaltBytes.AddRange(targetAsBytes);
            targetWithSaltBytes.AddRange(SaltBytes);
            byte[] digestBytes = hashAlgo.ComputeHash(targetWithSaltBytes.ToArray());

            return new HashWithSaltResult(Convert.ToBase64String(SaltBytes), Convert.ToBase64String(digestBytes));
        }
    }
}
