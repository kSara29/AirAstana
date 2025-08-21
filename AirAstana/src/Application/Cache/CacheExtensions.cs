using Application.Contracts.Services;
using Application.Models.DTO;

namespace Application.Cache;

public static class CacheExtensions
{
    private const string FlightsListIndex = "flights:list:index";
    
    public static async Task<T?> GetOrAddAsync<T>(this ICacheService cache, string key, Func<Task<T?>> factory, TimeSpan time)
    {
        var cachedData = cache.GetData<T>(key);
        if (cachedData is not null) return cachedData;

        var fresh = await factory();
        if (fresh is not null) 
            cache.SetData(key, fresh, time);
        
        return fresh;
    }

    public static void RemoveKeys(this ICacheService cache, params string[] keys)
    {
        foreach (var k in keys) 
            cache.RemoveData(k);
    }

    public static void UpsertEntity<T>(this ICacheService cache, string key, T value, TimeSpan time)
    {
        cache.SetData(key, value, time);
    }
    
    public static void UpdateById(this ICacheService cache, CreateFlightDto dto, TimeSpan time)
    {
        if (dto.Id is null) return;
        
        cache.UpsertEntity(CacheKeys.FlightById(dto.Id.Value), dto, time);
        cache.InvalidateAllLists();
    }
    

    public static void TrackListKey(this ICacheService cache, string key)
    {
        var set = cache.GetData<HashSet<string>>(FlightsListIndex) ?? new();
        if (set.Add(key))
            cache.SetData(FlightsListIndex, set, TimeSpan.FromDays(3650));
    }

    public static void InvalidateAllLists(this ICacheService cache)
    {
        var set = cache.GetData<HashSet<string>>(FlightsListIndex);
        if (set is null) return;
        foreach (var k in set) cache.RemoveData(k);
        cache.RemoveData(FlightsListIndex);
    }
}