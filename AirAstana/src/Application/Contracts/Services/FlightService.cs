using System.Runtime.CompilerServices;
using Application.Cache;
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
    private readonly ICacheService _cache;

    private static readonly TimeSpan time = TimeSpan.FromMinutes(180);
    public FlightService(IFlightRepository repository, IMapper mapper, ICacheService cache)
    {
        _repository = repository;
        _mapper = mapper;
        _cache = cache;
    }
    
    public async Task<List<FlightDto>?> GetAllAsync(CancellationToken ct)
    {
        
        return await _cache.GetOrAddAsync(
            CacheKeys.FlightsAll,
            async () => _mapper.Map<List<FlightDto>>(await _repository.GetAllAsync(ct) ?? new()),
            time);
    }

    public async Task<FlightDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await _cache.GetOrAddAsync(
            CacheKeys.FlightById(id),
            async () => _mapper.Map<FlightDto>(await _repository.GetByIdAsync(id, ct) ?? new()),
            time);
    }

    public async Task<DataResponse> CreateFlightAsync(FlightDto flight, CancellationToken ct)
    {
        try
        {
            var flightToDbFormat = _mapper.Map<Flight>(flight);
            var result = await _repository.CreateAsync(flightToDbFormat, ct);
            
            if (result == DbResults.Created)
            {
                var flightDto = _mapper.Map<FlightDto>(flightToDbFormat);
                _cache.UpdateById(flightDto, time);
                
                return new DataResponse() { CompletedCount = 1 };
            }
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
            {
                var flightDto = _mapper.Map<FlightDto>(flightToDbFormat);
                _cache.UpdateById(flightDto, time);
                
                return new DataResponse() { CompletedCount = 1 };
            }
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
            {
                _cache.RemoveKeys(id.ToString());
                return new DataResponse() { CompletedCount = 1 };
            }
               
        }
        catch (Exception ex)
        {
            return new DataResponse() { CompletedCount = 0, ErrorCount = 1, Errors = new List<string>(){ex.Message} };
        }

        return null;
    }
}