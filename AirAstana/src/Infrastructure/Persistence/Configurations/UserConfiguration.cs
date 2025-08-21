using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

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
        
        var hasher = new PasswordHasher<User>();
        
        var user1 = new User { Id = 1, Username = "moderator1", RoleId = 1 };
        user1.Password = hasher.HashPassword(user1, "QwertyModerator123");

        var user2 = new User { Id = 2, Username = "user1", RoleId = 2 };
        user2.Password = hasher.HashPassword(user2, "QwertyUserOne123@123");

        var user3 = new User { Id = 3, Username = "user2", RoleId = 2 };
        user3.Password = hasher.HashPassword(user3, "QwertyUserTwo123@123");

        builder.HasData(user1, user2, user3);
    }
}