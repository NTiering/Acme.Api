using FluentAssert;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Acme.Toolkit.Extensions
{
    public static class AssertExtensions
    {
        public static void ShouldContain<T>(this IEnumerable<T> item, T expected)
            where T : class
        {
            item.Contains(expected).ShouldBeTrue();
        }

        public static void ShouldNotContain<T>(this IEnumerable<T> item, T expected)
            where T : class
        {
            item.Contains(expected).ShouldBeFalse();
        }
    }
}