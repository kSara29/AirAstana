using Application.Contracts.Services;
using AutoMapper.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddScoped<IFlightService, FlightService>();
        return services;
    }
}