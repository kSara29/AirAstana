using Application.Contracts.Repositories;
using AutoMapper.Internal;
using Infrastructure.AutoMapper;
using Infrastructure.Cache.Configurations;
using Infrastructure.Cache.Extensions;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IFlightRepository, FlightRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddAutoMapper(m => m.Internal().MethodMappingEnabled = false, typeof(MappingProfile).Assembly);
        
        var redisOpt = config.GetSection("Redis").Get<RedisOptions>() ?? throw new InvalidOperationException("Redis is required");
        services.AddRedis(redisOpt);
        
        return services;
    }
}