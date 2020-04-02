using System;
using System.Linq;

namespace Acme.Toolkit.Extensions
{
    public static class StringExt
    {
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

        public static void ThrowIfNullOrEmpty(this string s, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(s) || string.IsNullOrEmpty(s))
            {
                throw new ArgumentNullException(parameterName);
            }
        }
    }
}