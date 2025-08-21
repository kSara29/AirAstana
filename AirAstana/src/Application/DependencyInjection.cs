using Application.Contracts.Services;
using Application.Contracts.Services.Interfaces;
using Application.Models;
using Application.Models.DTO;
using Application.Validation;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddScoped<IValidator<FlightDto>, FlightsValidator>();
        services.AddScoped<IValidator<EditFlightDto>, EditFlightDtoValidator>();
        services.AddScoped<IValidator<CreateFlightDto>, CreateFlightDtoValidator>();
        services.AddScoped<IValidator<LoginRequest>, LoginRequestValidator>();
        
        services.AddScoped<IFlightService, FlightService>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>(); 
        services.AddScoped<ITokenService, TokenService>();
        
        
        return services;
    }
}