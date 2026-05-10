using HomeServer.Core.Models;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeServer.Server.Data;

public class TodoCategoryDBConfiguration : IEntityTypeConfiguration<TodoCategory> {
    public void Configure(EntityTypeBuilder<TodoCategory> builder) {
        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(category => category.CreatedBy)
            .HasPrincipalKey(user => user.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(category => category.UpdatedBy)
            .HasPrincipalKey(user => user.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(category => category.Guid)
            .IsUnique();
        
        builder
            .HasAlternateKey(category => new { category.CreatedBy, category.Order }); // Unique constraint
    }
}