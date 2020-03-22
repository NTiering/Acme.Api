using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Acme.Toolkit.Extensions
{
    public static class AssemblyExt
    {
        public static IEnumerable<Type> GetTypesThatImplement<T>(this Assembly assembly)
        {
            var result = assembly
                .GetTypes()
                .Where(type => type.IsAbstract == false)
                .Where(type => type.IsClass == true)
                .Where(type => typeof(T).IsAssignableFrom(type));

            return result;
        }
    }
}