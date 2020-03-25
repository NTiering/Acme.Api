using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Acme.Encryption
{
    public class EncryptionProvider : IEncryptionProvider
    {
        private const string initVector = "H5dmefKm24mfTf5u";
        private const int keysize = 256;
        private const CipherMode cipherMode = CipherMode.CBC;

        public string Strategy => "RijndaelManaged-BCrypt";

        public string Decrypt(string cipherText, string passPhrase)
        {  

            var initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            var cipherTextBytes = Convert.FromBase64String(cipherText);
            var password = new PasswordDeriveBytes(passPhrase, null);
            var keyBytes = password.GetBytes(keysize / 8);
            var symmetricKey = new RijndaelManaged
            {
                Mode = cipherMode
            };
            var decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            var plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }

        public string Encrypt(string plainText, string passPhrase)
        {
            var initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var password = new PasswordDeriveBytes(passPhrase, null);
            var keyBytes = password.GetBytes(keysize / 8);
            var symmetricKey = new RijndaelManaged
            {
                Mode = cipherMode
            };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            var cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }

        public string Hash(string value)
        {
            var result = BCrypt.Net.BCrypt.HashPassword(value);
            return result;
        }

        public bool Verify(string value, string hash)
        {
            var result = BCrypt.Net.BCrypt.Verify(value, hash);
            return result;
        }
    }

}