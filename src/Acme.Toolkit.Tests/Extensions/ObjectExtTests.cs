using Acme.Tests;
using Acme.Toolkit.Extensions;
using System;
using Xunit;

namespace Acme.Toolkit.Tests
{
    public class ObjectExtTests
    {
        [Fact]
        public void ThrowExceptionOnNull()
        {
            Exception expected = null;
            try
            {
                expected.ThrowIfNull("param");
            }
            catch (Exception ex)
            {
                expected = ex;
            }

            expected.ShouldNotBeNull();
        }

        [Fact]
        public void WontThrowException()
        {
            "some string".ThrowIfNull("param");
        }
    }
}