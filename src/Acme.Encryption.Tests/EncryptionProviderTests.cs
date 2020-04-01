using System;
using System.Collections.Generic;
using FluentAssert;
using Xunit;

namespace Acme.Encryption.Tests
{
    public class EncryptionProviderTests
    {
        private IEncryptionProvider EncryptionProvider = new EncryptionProvider();
        private string password = "some passowrd";

        [Fact]
        public void ItCanHash()
        {
            EncryptionProvider.Hash("Some string").ShouldNotBeEmpty();
        }

        [Fact]
        public void ItCanHashRepeatably()
        {
            var hash = EncryptionProvider.Hash("Some string");

            EncryptionProvider.Hash("Some string").ShouldNotBeEqualTo(hash);
        }
        
        [Fact]
        public void ItCanEncrypt()
        {
            EncryptionProvider.Encrypt("Some string", password).ShouldNotBeEmpty();
        }

        [Fact]
        public void ItCanDecrypt()
        {
            var encrypted = EncryptionProvider.Encrypt("Some string", password);
            EncryptionProvider.Decrypt(encrypted, password).ShouldBeEqualTo("Some string");
        }

        [Fact]
        public void ItThrowsException()
        {
            Exception ex = null;
            try
            {
                var encrypted = EncryptionProvider.Encrypt("Some string", password);
                EncryptionProvider.Decrypt(encrypted, "not password");
            }
            catch (Exception e)
            {
                ex = e;
            }

            ex.ShouldNotBeNull();
        }

    }
}
