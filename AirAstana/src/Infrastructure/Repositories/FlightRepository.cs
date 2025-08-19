using Application.Contracts.Enums;
using Application.Contracts.Repositories;
using Domain.Models;
using Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;

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
        return await _dbContext.Flight.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id, ct);
    }

    public async Task<DbResults> UpdateAsync(Flight flight, CancellationToken ct)
    {
        var flightDb = await _dbContext.Flight.FirstOrDefaultAsync(f => f.Id == flight.Id, ct);
        _dbContext.Entry(flightDb).CurrentValues.SetValues(flight);
        await _dbContext.SaveChangesAsync(ct);

        return DbResults.Updated;
    }

    public async Task<DbResults> CreateAsync(Flight flight, CancellationToken ct)
    {
        var existFlight = await _dbContext.Flight
            .FirstOrDefaultAsync(f => f.Arrival == flight.Arrival
                                      && f.Departure == flight.Departure
                                      && f.Destination == flight.Destination
                                      && f.Origin == flight.Origin
                                      && f.Status == flight.Status);
        if (existFlight is not null)
            throw new Exception("Данный рейс уже существует!");
        
        await _dbContext.Flight.AddAsync(flight, ct);
        await _dbContext.SaveChangesAsync(ct);
        return DbResults.Created;
    }

    public async Task<DbResults> DeleteAsync(int id, CancellationToken ct)
    {
        var flightDb = await _dbContext.Flight.FirstOrDefaultAsync(f => f.Id == id, ct);
        
        if (flightDb is null)
            throw new Exception("Рейса с заданным айди не существует!");
        
        _dbContext.Flight.Remove(flightDb);
        await _dbContext.SaveChangesAsync(ct);

        return DbResults.Deleted;
    }
    
    public async Task<List<Flight>?> GetAllAsync(CancellationToken ct)
    {
        var flights = await _dbContext.Flight.AsNoTracking().ToListAsync(ct);

        return flights;
    }
}