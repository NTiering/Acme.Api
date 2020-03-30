using FluentAssert;
using Xunit;

namespace Acme.Encryption.Tests
{
    public class EncryptionProviderTests
    {
        private static readonly string salt = "E$$$_ sdsf ";

        [Fact]
        public void CanHash()
        {
            new EncryptionProvider().Hash("SomeTextHere").ShouldNotBeEqualTo("SomeTextHere");
        }

        [Fact]
        public void Decrypts()
        {
            var result = new EncryptionProvider().Encrypt("Some TextHere", salt);
            new EncryptionProvider().Decrypt(result, salt).ShouldBeEqualTo("Some TextHere");
        }

        [Fact]
        public void Encrypts()
        {
            new EncryptionProvider().Encrypt("Some Text HereLook", salt).ShouldNotBeNull();
        }

        [Fact]
        public void HashIsRepeatable()
        {
            var hash1 = new EncryptionProvider().Hash("SomeTextHere");
            var result = new EncryptionProvider().Verify("SomeTextHere", hash1);

            result.ShouldBeTrue();
        }

        [Fact]
        public void ReturnsStrategy()
        {
            new EncryptionProvider().Strategy.ShouldNotBeNull();
        }
    }
}