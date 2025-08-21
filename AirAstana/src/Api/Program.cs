using System.Security.Claims;
using Api.Filters;
using Application;
using Application.Auth;
using Application.Converters;
using Application.Validation;
using Domain.Enums;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Context;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog((context, services, cfg) =>
    cfg.ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext());

builder.Services.AddHttpContextAccessor();
builder.Logging.ClearProviders();

builder.Services.AddControllers(options =>
    {
        options.Filters.Add<FluentValidationFilter>();
    })
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(new SafeEnumConverter<FlightStatus>());
        //o.JsonSerializerOptions.Converters.Add(
        //    new System.Text.Json.Serialization.JsonStringEnumConverter(
        //        System.Text.Json.JsonNamingPolicy.CamelCase));
        o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidAudience = AuthOptions.AUDIENCE,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            RoleClaimType = ClaimTypes.Role
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API", 
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "JWT Bearer authorization",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddApplicationService();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddDbContext<Context>((sp, options) =>
{
    var cs = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(cs);
    options.AddInterceptors(sp.GetRequiredService<EfInterceptor>());
});


var app = builder.Build();

app.Use(async (ctx, next) =>
{
    var userName = ctx.User?.Identity?.IsAuthenticated == true
        ? (ctx.User.Identity?.Name ?? ctx.User.FindFirst("unique_name")?.Value ?? "auth")
        : "anonymous";

    using (LogContext.PushProperty("UserName", userName))
    {
        await next();
    }
});

app.UseExceptionHandler(a =>
{
    a.Run(async context =>
    {
        var ex = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;
        if (ex is not null)
            Log.Error(ex, "Unhandled exception");

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(new { error = "Internal Server Error" });
    });
});

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
    s.RoutePrefix = string.Empty;
});

app.UseAuthentication();   
app.UseAuthorization(); 

app.MapControllers();  

app.Run();