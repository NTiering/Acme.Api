using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;

namespace Acme.Caching
{
    public class CacheProvider : IDisposable, ICacheProvider
    {
        private readonly IMemoryCache memoryCache;

        public CacheProvider()
        {
            memoryCache = new MemoryCache(new DefaultOptions());
        }

        public void Dispose()
        {
            memoryCache.Dispose();
        }

        public void Expire(string key)
        {
            memoryCache.Remove(key);
        }

        public T Get<T>(string key, Func<T> onCacheExpire, CacheDuration cacheDuration)
                            where T : class
        {
            return TryGetValue(key, onCacheExpire, (int)cacheDuration);
        }

        public T Get<T>(string key, Func<T> onCacheExpire, int durationInMins)
            where T : class
        {
            return TryGetValue(key, onCacheExpire, durationInMins);
        }

        private T TryGetValue<T>(string key, Func<T> onCacheExpire, int duration) where T : class
        {
            if (memoryCache.TryGetValue(key, out T rtn) == false)
            {
                rtn = onCacheExpire();
                memoryCache.Set(key, rtn, new DateTimeOffset(DateTime.Now.AddMinutes(duration)));
            }
            return rtn;
        }

        private class DefaultOptions : IOptions<MemoryCacheOptions>
        {
            public DefaultOptions()
            {
                Value = new MemoryCacheOptions();
            }

            public MemoryCacheOptions Value { get; }
        }
    }
}