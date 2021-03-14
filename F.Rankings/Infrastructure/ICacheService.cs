using System;

namespace F.Rankings.Infrastructure
{
    public interface ICacheService
    {
        bool TryGet<T>(string cacheKey, out T item) where T : class;
        bool TryAdd<T>(string cacheKey, T item) where T : class;
    }
}