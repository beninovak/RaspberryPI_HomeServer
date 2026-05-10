using HomeServer.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeServer.Server.Data;

public class UserDBConfiguration : IEntityTypeConfiguration<User> {
    public void Configure(EntityTypeBuilder<User> builder) {
        builder
            .HasIndex(user => user.Guid)
            .IsUnique();

        builder
            .HasIndex(user => user.Email)
            .IsUnique();
    }
}