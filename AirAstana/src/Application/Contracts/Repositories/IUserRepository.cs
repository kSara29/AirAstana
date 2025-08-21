using Domain.Models;

namespace Application.Contracts.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserAsync(string username, CancellationToken ct = default);
}