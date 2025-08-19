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

    public async Task<DbResults> CreateAsync(Flight flight, CancellationToken ct)
    {
        if (flight is null)
            throw new ArgumentNullException(nameof(flight));
        
        await _dbContext.Flight.AddAsync(flight, ct);
        await _dbContext.SaveChangesAsync(ct);

        return DbResults.Created;
    }

    public async Task<Flight?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await _dbContext.Flight.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id, ct);
    }

    public async Task<DbResults> UpsertAsync(Flight flight, CancellationToken ct)
    {
        if (flight is null) 
            throw new ArgumentNullException(nameof(flight));
        
        var flightDb = await _dbContext.Flight.FirstOrDefaultAsync(f => f.Id == flight.Id, ct);

        if (flightDb is null)
        {
            await _dbContext.Flight.AddAsync(flight, ct);
            await _dbContext.SaveChangesAsync(ct);
            return DbResults.Created;
        }
        
        _dbContext.Entry(flightDb).CurrentValues.SetValues(flight);
        await _dbContext.SaveChangesAsync(ct);

        return DbResults.Updated;
    }

    public async Task<List<Flight>?> GetAllAsync(CancellationToken ct)
    {
        var flights = await _dbContext.Flight.AsNoTracking().ToListAsync(ct);

        return flights;
    }
}