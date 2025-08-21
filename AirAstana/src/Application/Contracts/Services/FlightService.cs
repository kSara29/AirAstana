using Application.Cache;
using Application.Contracts.Enums;
using Application.Contracts.Repositories;
using Application.Models;
using Application.Models.DTO;
using AutoMapper;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Contracts.Services;

public class FlightService : IFlightService
{
    private readonly IFlightRepository _repository;
    private readonly IMapper _mapper;
    private readonly ICacheService _cache;
    private readonly ILogger<FlightService> _logger;

    private static readonly TimeSpan time = TimeSpan.FromMinutes(180);
    public FlightService(IFlightRepository repository, IMapper mapper, ICacheService cache, ILogger<FlightService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _cache = cache;
        _logger = logger;
    }
    
    public async Task<List<CreateFlightDto>?> GetAllAsync(string? origin, string? destination, bool desc, CancellationToken ct)
    {
        try
        {
            var cacheKey = CacheKeys.FlightsList(origin, destination, desc);
            
            var items= await _cache.GetOrAddAsync(
                cacheKey,
                async () =>
                {
                    var entity = await _repository.GetAllAsync(origin, destination, desc, ct);
                    return entity is null ? null : _mapper.Map<List<CreateFlightDto>>(entity);
                },
                time);
            _cache.TrackListKey(cacheKey);

            return items;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetFlights error");
            return null;
        }
    }

    public async Task<CreateFlightDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        if (id <= 0) return null;
        
        try
        {
            return await _cache.GetOrAddAsync(
                CacheKeys.FlightById(id),
                async () =>
                {
                    var entity = await _repository.GetByIdAsync(id, ct);
                    return entity is null ? null : _mapper.Map<CreateFlightDto>(entity);
                },
                time);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"GetFlightById error: id={id}");
            return null;
        }
    }

    public async Task<DataResponse> CreateFlightAsync(CreateFlightDto createFlight, CancellationToken ct)
    {
        try
        {
            var flightToDbFormat = _mapper.Map<Flight>(createFlight);
            var result = await _repository.CreateAsync(flightToDbFormat, ct);
            
            if (result == DbResults.Created)
            {
                var flightDto = _mapper.Map<CreateFlightDto>(flightToDbFormat);
                _cache.UpdateById(flightDto, time);
                
                return new DataResponse() { CompletedCount = 1 };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"CreateFlight failed: flight={createFlight}");
            return new DataResponse() { CompletedCount = 0, ErrorCount = 1, Errors = new List<string>(){ex.Message} };
        }

        return new DataResponse() { CompletedCount = 0 };
    }

    public async Task<DataResponse> UpdateFlightAsync(int id, EditFlightDto status, CancellationToken ct)
    {
        try
        {
            var result = await _repository.UpdateAsync(id, status.Status, ct);

            if (result is not null)
            {
                var flightDto = _mapper.Map<CreateFlightDto>(result);
                _cache.UpdateById(flightDto, time);
                
                return new DataResponse() { CompletedCount = 1 };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"UpdateFlight failed: flightId={id}");
            return new DataResponse() { CompletedCount = 0, ErrorCount = 1, Errors = new List<string>(){ex.Message} };
        }

        return new DataResponse() { CompletedCount = 0 };
    }

    public async Task<DataResponse> DeleteFlightAsync(int id, CancellationToken ct)
    {
        try
        {
            var result = await _repository.DeleteAsync(id, ct);

            if (result == DbResults.Deleted)
            {
                _cache.RemoveKeys(CacheKeys.FlightById(id), CacheKeys.FlightsAll);
                _cache.InvalidateAllLists();
                return new DataResponse() { CompletedCount = 1 };
            }
               
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"DeleteFlight failed: flightId={id}");
            return new DataResponse() { CompletedCount = 0, ErrorCount = 1, Errors = new List<string>(){ex.Message} };
        }

        return new DataResponse() { CompletedCount = 0 };
    }
}