using System;
using System.Runtime.Caching;

namespace F.Rankings.Infrastructure
{
    public class InMemoryCacheService : ICacheService
    {
        private readonly CacheItemPolicy _policy;

        public InMemoryCacheService(CacheItemPolicy policy)
        {
            _policy = policy;
        }

        public bool TryAdd<T>(string cacheKey, T item) where T : class
        {
            return MemoryCache.Default.Add(cacheKey, item, _policy);
        }

        public bool TryGet<T>(string cacheKey, out T item) where T : class
        {
            item = MemoryCache.Default.Get(cacheKey) as T;
            return item != null;
        }
    }
}
