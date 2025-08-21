using Application.Models;
using Application.Models.DTO;
using Domain.Models;

namespace Application.Contracts.Services;

public interface IFlightService
{
    Task<List<FlightDto>?> GetAllAsync(CancellationToken ct = default);
    Task<FlightDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<DataResponse> CreateFlightAsync(FlightDto flight, CancellationToken ct = default);
    Task<DataResponse> UpdateFlightAsync(int id, EditFlightDto status, CancellationToken ct = default);
    Task<DataResponse> DeleteFlightAsync(int id, CancellationToken ct = default);
}