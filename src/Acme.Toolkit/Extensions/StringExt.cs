using Acme.Encryption;
using System.Linq;

namespace Acme.Toolkit.Extensions
{
    public static class StringExt
    {
        public static string DecryptText(this string value, string key)
        {
            return new EncryptionProvider().Decrypt(value, key.TakeMin(21));
        }

        public static string EncryptText(this string value, string key)
        {
            return new EncryptionProvider().Encrypt(value, key.TakeMin(21));
        }

        public static string TakeMin(this string item, int count)
        {
            var value = new string(item.ToArray());

            if (value.Length >= count)
            {
                return value.Substring(0, count);
            }
            else
            {
                while (value.Length < count)
                {
                    value += new string(item.ToArray());
                }

                return value.Substring(0, count);
            }
        }
    }
}