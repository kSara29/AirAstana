using Application.Contracts.Repositories;
using AutoMapper.Internal;
using Infrastructure.AutoMapper;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
   
        services.AddScoped<IFlightRepository, FlightRepository>();
        services.AddAutoMapper(m => m.Internal().MethodMappingEnabled = false, typeof(MappingProfile).Assembly);
        return services;
    }
}