using Application.Contracts.Enums;
using Domain.Models;

namespace Application.Contracts.Repositories;

public interface IFlightRepository
{
    Task<DbResults> CreateAsync(Flight flight, CancellationToken ct = default);
    Task<Flight?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<DbResults> UpsertAsync(Flight flight, CancellationToken ct = default);
    Task<List<Flight>?> GetAllAsync(CancellationToken ct = default);
}