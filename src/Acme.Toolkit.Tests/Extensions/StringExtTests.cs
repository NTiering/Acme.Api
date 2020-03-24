using Acme.Toolkit.Extensions;
using FluentAssert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Acme.Toolkit.Tests.Extensions
{
    public class StringExtTests
    {
        [Fact]
        public void ItCanEncryptAndDecrypt()
        {
            // arrange 
            var expected = "asdad ad asd sd as d";
            var toEncrypt = new string(expected.ToArray());

            var key = "afddsasSaq3242324wmcw";
           
            // act 
            var actual = toEncrypt.EncryptText(key).DecryptText(key);

            // assert
            actual.ShouldBeEqualTo(expected);
        }

        [Fact]
        public void TakeMinWorks()
        {
            "A12345".TakeMin(6).ShouldBeEqualTo("A12345");
            "B123456".TakeMin(6).ShouldBeEqualTo("B12345");
            "C123".TakeMin(6).ShouldBeEqualTo("C123C1");
            "1".TakeMin(6).ShouldBeEqualTo("111111");
            "afddsasSaq3242324wmcw".TakeMin(21).ShouldBeEqualTo("afddsasSaq3242324wmcw");

        }
    }
}

        
