using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);
        
        builder.Property(r => r.Code)
            .HasMaxLength(256)
            .IsRequired();

        builder.HasIndex(r => r.Code).IsUnique();

        builder.HasData(
            new Role { Id = 1, Code = "Moderator" },
            new Role { Id = 2, Code = "User" }
        );
    }
}