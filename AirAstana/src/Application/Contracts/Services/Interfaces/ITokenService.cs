using System.Security.Claims;

namespace Application.Contracts.Services.Interfaces;

public interface ITokenService
{
    public Task<object?> TokenAsync(string username, string password, CancellationToken ct = default);
    Task<ClaimsIdentity?> GetIdentityAsync(string username, string password, CancellationToken ct = default);
}