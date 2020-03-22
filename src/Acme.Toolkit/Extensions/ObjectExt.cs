using System;

namespace Acme.Toolkit.Extensions
{
    public static class ObjectExt
    {
        public static void ThrowIfNull<T>(this T obj, string parameterName) where T : class
        {
            if (obj == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
    }
}