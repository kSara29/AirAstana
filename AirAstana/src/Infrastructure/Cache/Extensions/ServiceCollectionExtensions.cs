using Application.Contracts.Services;
using Infrastructure.Cache.Configurations;
using Infrastructure.Cache.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Cache.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRedis(this IServiceCollection services, RedisOptions redisOptions)
    {
        services.AddStackExchangeRedisCache(o =>
        {
            o.Configuration = redisOptions.Endpoint;
        });
        
        services.AddSingleton<ICacheService, RedisCacheService>();
        return services;
    }
}