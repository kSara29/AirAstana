using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.Username)
            .HasMaxLength(256)
            .IsRequired();
        
        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(256);
        
        builder.HasIndex(u => u.Username).IsUnique();
    }
}