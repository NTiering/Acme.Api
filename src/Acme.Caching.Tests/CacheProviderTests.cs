using Acme.Caching;
using Acme.Tests;
using Moq;
using System;
using Xunit;

namespace MMU.BusSys.Caching.UnitTests
{
    public class CacheProviderTests : IDisposable
    {
        private static readonly string cacheKey = "SOME-KEY";
        private readonly MockRepository mockRepository;

        public CacheProviderTests()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
        }

        public void Dispose()
        {
            mockRepository.VerifyAll();
        }

        [Fact]
        public void ExpiresRemovesCachedValue()
        {
            // Arrange
            var entry1 = new MockCacheEntry();
            var entry2 = new MockCacheEntry();
            MockCacheEntry result;
            MockCacheEntry onCacheExpire1() { return entry1; }
            MockCacheEntry onCacheExpire2() { return entry2; }

            using (var provider = new CacheProvider())
            {
                // First call
                provider.Get(
                     cacheKey,
                     onCacheExpire1,
                     CacheDuration.Short);

                provider.Expire(cacheKey);

                // Act
                result = provider.Get(
                     cacheKey,
                     onCacheExpire2,
                     CacheDuration.Short);
            }

            // Assert
            result.Id.ShouldBeEqualTo(entry2.Id);
        }

        [Fact]
        public void GetReturnsCachedValue()
        {
            // Arrange
            var entry = new MockCacheEntry();
            MockCacheEntry result;
            MockCacheEntry onCacheExpireResult() { return entry; }
            MockCacheEntry onCacheExpireThrowException() { throw new Exception(); }

            using (var provider = new CacheProvider())
            {
                // First call
                provider.Get(
                     cacheKey,
                     onCacheExpireResult,
                     CacheDuration.Short);

                // Act
                result = provider.Get(
                     cacheKey,
                     onCacheExpireThrowException,
                     CacheDuration.Short);
            }

            // Assert
            result.Id.ShouldBeEqualTo(entry.Id);
        }

        [Fact]
        public void GetReturnsFunctionResult()
        {
            // Arrange
            var entry = new MockCacheEntry();
            MockCacheEntry result;
            MockCacheEntry onCacheExpire() { return entry; }

            using (var provider = new CacheProvider())
            {
                // Act
                result = provider.Get(
                     cacheKey,
                     onCacheExpire,
                     CacheDuration.Short);
            }

            // Assert
            result.Id.ShouldBeEqualTo(entry.Id);
        }

        public class MockCacheEntry
        {
            public MockCacheEntry()
            {
                Id = Guid.NewGuid().ToString();
            }

            public string Id { get; set; }
        }
    }
}