using Domain.Contracts.CacheRepositorys;
using Services.Abstraction.Interface;

namespace Services.Implementation
{
    public class CacheService(ICacheRepository _cacheRepository) : ICacheService
    {
        public async Task<string?> GetCacheValueAsync(string key) => await _cacheRepository.GetAsync(key);

        public Task SetCacheValueAsync(string key, object value, TimeSpan duration) => _cacheRepository.SetAsync(key, value, duration);

    }
}
