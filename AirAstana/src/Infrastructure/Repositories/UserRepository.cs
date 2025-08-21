using Application.Contracts.Repositories;
using Domain.Models;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly Context _db;

    public UserRepository(Context db)
    {
        _db = db;
    }

    public async Task<User?> GetUserAsync(string username, CancellationToken ct)
    {
        var user = await _db.Users
            .Include(u => u.Role)            
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Username == username, ct);

        return user;
    }
}
