using HomeServer.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeServer.Data.Users;

public class UserDBConfiguration : IEntityTypeConfiguration<User> {
    public void Configure(EntityTypeBuilder<User> builder) {
        builder
            .HasKey(user => user.Guid);
        
        builder
            .HasIndex(user => user.Email)
            .IsUnique();
    }
}