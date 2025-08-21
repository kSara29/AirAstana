using Application.Models;
using Application.Models.DTO;
using Domain.Models;

namespace Application.Contracts.Services;

public interface IFlightService
{
    Task<List<CreateFlightDto>?> GetAllAsync(string? origin, string? destination, bool desc, CancellationToken ct = default);
    Task<CreateFlightDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<DataResponse> CreateFlightAsync(CreateFlightDto createFlight, CancellationToken ct = default);
    Task<DataResponse> UpdateFlightAsync(int id, EditFlightDto status, CancellationToken ct = default);
    Task<DataResponse> DeleteFlightAsync(int id, CancellationToken ct = default);
}