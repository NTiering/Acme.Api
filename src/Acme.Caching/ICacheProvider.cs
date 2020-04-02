using System;

namespace Acme.Caching
{
    public interface ICacheProvider
    {
        void Dispose();

        void Expire(string key);

        T Get<T>(string key, Func<T> onCacheExpire, CacheDuration cacheDuration) where T : class;

        T Get<T>(string key, Func<T> onCacheExpire, int durationInMins) where T : class;
    }
}