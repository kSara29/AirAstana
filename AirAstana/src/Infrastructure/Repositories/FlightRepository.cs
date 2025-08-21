using Application.Contracts.Enums;
using Application.Contracts.Repositories;
using Domain.Enums;
using Domain.Models;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

public class FlightRepository : IFlightRepository
{
    private readonly Context _dbContext;

    public FlightRepository(Context dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Flight?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await _dbContext.Flights.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id, ct);
    }

    public async Task<Flight?> UpdateAsync(int id, FlightStatus newStatus, CancellationToken ct)
    {
        var flightDb = await _dbContext.Flights.FirstOrDefaultAsync(f => f.Id == id, ct);
        if (flightDb is null)
            throw new InvalidOperationException($"Flight with id {id} does not exist!");
        
        flightDb.Status = newStatus;
        await _dbContext.SaveChangesAsync(ct);

        return flightDb;
    }

    public async Task<DbResults> CreateAsync(Flight flight, CancellationToken ct)
    {
        var existFlight = await _dbContext.Flights
            .AnyAsync(f => 
                f.Arrival == flight.Arrival
                && f.Departure == flight.Departure
                && f.Destination == flight.Destination
                && f.Origin == flight.Origin
                && f.Status == flight.Status, ct);
        
        if (existFlight)
            throw new InvalidOperationException("Flight already exist!");
        
        await _dbContext.Flights.AddAsync(flight, ct);
        await _dbContext.SaveChangesAsync(ct);
        return DbResults.Created;
    }

    public async Task<DbResults> DeleteAsync(int id, CancellationToken ct)
    {
        var flightDb = await _dbContext.Flights.FirstOrDefaultAsync(f => f.Id == id, ct);
        
        if (flightDb is null)
            throw new InvalidOperationException($"Flight with id {id} does not exist!");
        
        _dbContext.Flights.Remove(flightDb);
        await _dbContext.SaveChangesAsync(ct);

        return DbResults.Deleted;
    }
    
    public async Task<List<Flight>?> GetAllAsync(string? origin, string? destination, bool desc, CancellationToken ct)
    {
        var flights = _dbContext.Flights.AsNoTracking();
        
        if (!string.IsNullOrWhiteSpace(origin))
            flights = flights.Where(f => f.Origin == origin);

        if (!string.IsNullOrWhiteSpace(destination))
            flights = flights.Where(f => f.Destination == destination);

        flights = desc ? flights.OrderByDescending(f => f.Arrival) : flights.OrderBy(f => f.Arrival);

        return await flights.ToListAsync(ct);
    }
}