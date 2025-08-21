using Application.Contracts.Services;
using Application.Contracts.Services.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddScoped<IFlightService, FlightService>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>(); 
        services.AddScoped<ITokenService, TokenService>();
        
        return services;
    }
}