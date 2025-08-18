using System.Collections.Immutable;
using System.Reflection;
using Domain.Models;
using Infrastructure.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Context;

public class Context : DbContext
{
    public DbSet<Flight> Flight { get; set; }
    public DbSet<User> User { get; set; }
    
    public Context(DbContextOptions<Context> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(builder);
    }
}