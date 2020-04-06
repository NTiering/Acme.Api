using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Acme.Tests
{
    public static class AssertExtensions
    {
        public static void ShouldBeEqualTo<T>(this T original, T testSubject)
        {
            Assert.Equal(original, testSubject);
        }

        public static void ShouldNotBeEqualTo<T>(this T original, T testSubject)
        {
            Assert.NotEqual(original, testSubject);
        }

        public static void ShouldBeFalse(this bool i)
        {
            Assert.False(i);
        }

        public static void ShouldBeNull<T>(this T i)
            where T : class
        {
            Assert.Null(i);
        }

        public static void ShouldBeTrue(this bool i)
        {
            Assert.True(i);
        }

        public static void ShouldContain<T>(this IEnumerable<T> item, T expected)
                                    where T : class
        {
            item.Contains(expected).ShouldBeTrue();
        }

        public static void ShouldNotBeNull<T>(this T i)
            where T : class
        {
            Assert.NotNull(i);
        }

        public static void ShouldNotBeEmpty(this string i)
        {
            Assert.NotEmpty(i);
        }
        

        public static void ShouldNotContain<T>(this IEnumerable<T> item, T expected)
                    where T : class
        {
            item.Contains(expected).ShouldBeFalse();
        }
    }
}