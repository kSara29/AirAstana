using Application.Contracts.Enums;
using Application.Models.DTO;
using Domain.Enums;
using Domain.Models;

namespace Application.Contracts.Repositories;

public interface IFlightRepository
{
    Task<Flight?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Flight?> UpdateAsync(int id, FlightStatus newStatus, CancellationToken ct = default);
    Task<DbResults> CreateAsync(Flight flight, CancellationToken ct = default);
    Task<DbResults> DeleteAsync(int id, CancellationToken ct = default);
    Task<List<Flight>?> GetAllAsync(string? origin, string? destination, bool desc, CancellationToken ct = default);
}