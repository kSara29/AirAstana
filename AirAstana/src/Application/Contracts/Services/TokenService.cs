using System.Security.Claims;
using Application.Contracts.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Application.Auth;
using Application.Contracts.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Application.Contracts.Services;

public class TokenService : ITokenService
{
    private readonly IUserRepository _users;
    private readonly IPasswordHasher<User> _hasher;
    
    public TokenService(IUserRepository users, IPasswordHasher<User> hasher)
    {
        _users = users;
        _hasher = hasher;
    }
    
    public async  Task<object?> TokenAsync(string username, string password, CancellationToken ct)
    {
        var identity = await GetIdentityAsync(username, password, ct);
        if (identity is null) return null;
        
        var now = DateTime.UtcNow;
        
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience:AuthOptions.AUDIENCE,
            claims:identity.Claims,
            notBefore:now,
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        var response = new
        {
            access_token = encodedJwt,
            token_type = "Bearer",
            username = identity.Name
        };

        return response;
    }

    public async Task<ClaimsIdentity?> GetIdentityAsync(string username, string password, CancellationToken ct)
    {
        var user = await _users.GetUserAsync(username, ct);
        if (user is null) return null;

        var verify = _hasher.VerifyHashedPassword(user, user.Password, password);
        if (verify == PasswordVerificationResult.Failed) return null;

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Role, user.Role.Code)
        };

        return new ClaimsIdentity(claims, "Jwt", ClaimTypes.Name, ClaimTypes.Role);
    }
}