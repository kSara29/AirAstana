using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations;

public class FlightConfiguration : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        builder.HasKey(f => f.Id);
        
        builder.Property(f => f.Origin)
            .HasMaxLength(256)
            .IsRequired();
        
        builder.Property(f => f.Destination)
            .HasMaxLength(256)
            .IsRequired();
        
        builder.Property(f => f.Departure)
            .HasColumnType("timestamptz")
            .IsRequired();
        
        builder.Property(f => f.Arrival)
            .HasColumnType("timestamptz")
            .IsRequired();

        builder.Property(f => f.Status)
            .HasConversion<string>()
            .IsRequired();
    }
}