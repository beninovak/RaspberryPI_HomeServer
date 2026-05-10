using HomeServer.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeServer.Server.Data;

public class ExpenseCategoryDBConfiguration : IEntityTypeConfiguration<ExpenseCategory> {
    public void Configure(EntityTypeBuilder<ExpenseCategory> builder) {
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