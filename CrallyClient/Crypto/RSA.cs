using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Crypto
{
    static class RSA
    {
        public static byte[] RSAEncrypt(byte[] plaintext, string destKey)
        {
            byte[] encryptedData;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(destKey);
            encryptedData = rsa.Encrypt(plaintext, false);
            rsa.Dispose();
            return encryptedData;
        }
    }
}
