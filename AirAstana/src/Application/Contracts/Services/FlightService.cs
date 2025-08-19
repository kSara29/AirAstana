using System.Runtime.CompilerServices;
using Application.Contracts.Enums;
using Application.Contracts.Repositories;
using Application.Models;
using Application.Models.DTO;
using AutoMapper;
using Domain.Models;

namespace Application.Contracts.Services;

public class FlightService : IFlightService
{
    private readonly IFlightRepository _repository;
    private readonly IMapper _mapper;

    public FlightService(IFlightRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<List<FlightDto>?> GetAllAsync(CancellationToken ct)
    {
        var flights = await _repository.GetAllAsync(ct);
        var result = _mapper.Map<List<FlightDto>>(flights);

        return result;
    }

    public async Task<FlightDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        var flight = await _repository.GetByIdAsync(id, ct);
        var result = _mapper.Map<FlightDto>(flight);
        
        return result;
    }

    public async Task<DataResponse> CreateFlightAsync(FlightDto flight, CancellationToken ct)
    {
        try
        {
            var flightToDbFormat = _mapper.Map<Flight>(flight);
            var result = await _repository.CreateAsync(flightToDbFormat, ct);
            if (result == DbResults.Created)
                return new DataResponse() { CompletedCount = 1 };
        }
        catch (Exception ex)
        {
            return new DataResponse() { CompletedCount = 0, ErrorCount = 1, Errors = new List<string>(){ex.Message} };
        }

        return null;
    }

    public async Task<DataResponse> UpdateFlightAsync(FlightDto flight, CancellationToken ct)
    {
        try
        {
            var flightToDbFormat = _mapper.Map<Flight>(flight);
            var result = await _repository.UpdateAsync(flightToDbFormat, ct);
            if (result == DbResults.Updated)
                return new DataResponse() { CompletedCount = 1 };
        }
        catch (Exception ex)
        {
            return new DataResponse() { CompletedCount = 0, ErrorCount = 1, Errors = new List<string>(){ex.Message} };
        }

        return null;
    }

    public async Task<DataResponse> DeleteFlightAsync(int id, CancellationToken ct)
    {
        try
        {
            var result = await _repository.DeleteAsync(id, ct);

            if (result == DbResults.Deleted)
                return new DataResponse() { CompletedCount = 1 };
        }
        catch (Exception ex)
        {
            return new DataResponse() { CompletedCount = 0, ErrorCount = 1, Errors = new List<string>(){ex.Message} };
        }

        return null;
    }
}