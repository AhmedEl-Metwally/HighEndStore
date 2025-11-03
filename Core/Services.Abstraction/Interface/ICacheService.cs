
namespace Services.Abstraction.Interface
{
    public interface ICacheService
    {
        Task<string?> GetCacheValueAsync(string key);
        Task SetCacheValueAsync(string key, object value, TimeSpan duration);
    }
}
