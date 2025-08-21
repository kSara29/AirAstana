using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

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
        
        
        builder.HasData(
            new Flight
            {
                Id = 1,
                Origin = "Almaty",
                Destination = "Astana",
                Departure = new DateTimeOffset(2025, 9, 1, 9, 15, 0, new TimeSpan(5, 0, 0)),
                Arrival   = new DateTimeOffset(2025, 9, 1, 11,  0, 0, new TimeSpan(5, 0, 0)),
                Status = FlightStatus.InTime
            },
            new Flight
            {
                Id = 2,
                Origin = "Astana",
                Destination = "Almaty",
                Departure = new DateTimeOffset(2025, 9, 1, 18, 45, 0, new TimeSpan(5, 0, 0)),
                Arrival   = new DateTimeOffset(2025, 9, 1, 20, 30, 0, new TimeSpan(5, 0, 0)),
                Status = FlightStatus.InTime
            },
            new Flight
            {
                Id = 3,
                Origin = "Almaty",
                Destination = "Shymkent",
                Departure = new DateTimeOffset(2025, 9, 2, 7, 30, 0, new TimeSpan(5, 0, 0)),
                Arrival   = new DateTimeOffset(2025, 9, 2, 8, 50, 0, new TimeSpan(5, 0, 0)),
                Status = FlightStatus.InTime
            },
            new Flight
            {
                Id = 4,
                Origin = "Astana",
                Destination = "Aktau",
                Departure = new DateTimeOffset(2025, 9, 2, 10, 20, 0, new TimeSpan(5, 0, 0)),
                Arrival   = new DateTimeOffset(2025, 9, 2, 12, 20, 0, new TimeSpan(5, 0, 0)),
                Status = FlightStatus.InTime
            },
            new Flight
            {
                Id = 5,
                Origin = "Atyrau",
                Destination = "Astana",
                Departure = new DateTimeOffset(2025, 9, 3, 6, 40, 0, new TimeSpan(5, 0, 0)),
                Arrival   = new DateTimeOffset(2025, 9, 3, 8, 40, 0, new TimeSpan(5, 0, 0)),
                Status = FlightStatus.Delayed
            },
            new Flight
            {
                Id = 6,
                Origin = "Almaty",
                Destination = "Dubai",
                Departure = new DateTimeOffset(2025, 9, 3, 22, 10, 0, new TimeSpan(5, 0, 0)),
                Arrival   = new DateTimeOffset(2025, 9, 3, 23, 55, 0, new TimeSpan(4, 0, 0)), 
                Status = FlightStatus.InTime
            },
            new Flight
            {
                Id = 7,
                Origin = "Astana",
                Destination = "Istanbul",
                Departure = new DateTimeOffset(2025, 9, 4, 12, 35, 0, new TimeSpan(5, 0, 0)),
                Arrival   = new DateTimeOffset(2025, 9, 4, 14, 25, 0, new TimeSpan(3, 0, 0)), 
                Status = FlightStatus.InTime
            },
            new Flight
            {
                Id = 8,
                Origin = "Almaty",
                Destination = "Frankfurt",
                Departure = new DateTimeOffset(2025, 9, 5, 8, 0, 0, new TimeSpan(5, 0, 0)),
                Arrival   = new DateTimeOffset(2025, 9, 5, 11, 30, 0, new TimeSpan(2, 0, 0)), 
                Status = FlightStatus.InTime
            },
            new Flight
            {
                Id = 9,
                Origin = "Astana",
                Destination = "London",
                Departure = new DateTimeOffset(2025, 9, 6, 13, 15, 0, new TimeSpan(5, 0, 0)),
                Arrival   = new DateTimeOffset(2025, 9, 6, 16, 20, 0, new TimeSpan(1, 0, 0)), 
                Status = FlightStatus.Cancelled
            },
            new Flight
            {
                Id = 10,
                Origin = "Almaty",
                Destination = "Seoul",
                Departure = new DateTimeOffset(2025, 9, 7, 1, 20, 0, new TimeSpan(5, 0, 0)),
                Arrival   = new DateTimeOffset(2025, 9, 7, 10, 5, 0, new TimeSpan(9, 0, 0)),   
                Status = FlightStatus.InTime
            }
        );
    }
}