using System.Text.Json;
using Application.Contracts.Services;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Cache.Redis;

public class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _cache;

    public RedisCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }
    public T GetData<T>(string key)
    {
        try
        {
            var value = _cache.GetString(key);
            return !string.IsNullOrEmpty(value) ? JsonSerializer.Deserialize<T>(value) : default;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occuring while receiving data from Reddis by key {key}: {ex.Message}");
            return default;
        }
    }

    public bool SetData<T>(string key, T value, TimeSpan expirationTime)
    {
        try
        {
            var timeOut = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = expirationTime
            };
            _cache.SetString(key, JsonSerializer.Serialize(value), timeOut);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occuring while setting data to Reddis by key {key}: {ex.Message}");
            return false;
        }
    }

    public object RemoveData(string key)
    {
        try
        {
            _cache.Remove(key);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occuring while removing data from Reddis by key {key}: {ex.Message}");
            return false;
        }
    }
}