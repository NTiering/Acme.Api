﻿using Acme.Toolkit.Extensions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Acme.Encryption
{
    public class EncryptionProvider : IEncryptionProvider
    {
        public string Strategy => "SHA512";

        public string Decrypt(string value, string key)
        {
            value.ThrowIfNullOrEmpty(nameof(value));

            var combined = Convert.FromBase64String(value);
            var buffer = new byte[combined.Length];
            var hash = new SHA512CryptoServiceProvider();
            var aesKey = new byte[24];
            Buffer.BlockCopy(hash.ComputeHash(Encoding.UTF8.GetBytes(key)), 0, aesKey, 0, 24);

            using (var aes = Aes.Create())
            {
                if (aes == null)
                    throw new ArgumentException("Parameter must not be null.", nameof(aes));

                aes.Key = aesKey;

                var iv = new byte[aes.IV.Length];
                var ciphertext = new byte[buffer.Length - iv.Length];

                Array.ConstrainedCopy(combined, 0, iv, 0, iv.Length);
                Array.ConstrainedCopy(combined, iv.Length, ciphertext, 0, ciphertext.Length);

                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var resultStream = new MemoryStream())
                {
                    using (var aesStream = new CryptoStream(resultStream, decryptor, CryptoStreamMode.Write))
                    using (var plainStream = new MemoryStream(ciphertext))
                    {
                        plainStream.CopyTo(aesStream);
                    }

                    return Encoding.UTF8.GetString(resultStream.ToArray());
                }
            }
        }

        public string Encrypt(string text, string key)
        {
            key.ThrowIfNullOrEmpty(nameof(key));
            text.ThrowIfNullOrEmpty(nameof(text));

            var buffer = Encoding.UTF8.GetBytes(text);
            var hash = new SHA512CryptoServiceProvider();
            var aesKey = new byte[24];
            Buffer.BlockCopy(hash.ComputeHash(Encoding.UTF8.GetBytes(key)), 0, aesKey, 0, 24);

            using var aes = Aes.Create();
            aes.ThrowIfNull("AES");

            aes.Key = aesKey;

            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            using (var resultStream = new MemoryStream())
            {
                using (var aesStream = new CryptoStream(resultStream, encryptor, CryptoStreamMode.Write))
                using (var plainStream = new MemoryStream(buffer))
                {
                    plainStream.CopyTo(aesStream);
                }

                var result = resultStream.ToArray();
                var combined = new byte[aes.IV.Length + result.Length];
                Array.ConstrainedCopy(aes.IV, 0, combined, 0, aes.IV.Length);
                Array.ConstrainedCopy(result, 0, combined, aes.IV.Length, result.Length);

                return Convert.ToBase64String(combined);
            }
        }

        public string Hash(string value)
        {
            value.ThrowIfNullOrEmpty(nameof(value));

            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: value,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}