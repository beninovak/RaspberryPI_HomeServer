using HomeServer.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeServer.Server.Data;

public class ExpenseTargetDBConfiguration : IEntityTypeConfiguration<ExpenseTarget> {
    public void Configure(EntityTypeBuilder<ExpenseTarget> builder) {
        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(target => target.CreatedBy)
            .HasPrincipalKey(user => user.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(target => target.UpdatedBy)
            .HasPrincipalKey(user => user.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne<ExpenseCategory>()
            .WithMany()
            .HasForeignKey(target => target.ExpenseCategoryId)
            .HasPrincipalKey(category => category.Id);
        
        builder
            .HasIndex(target => target.Guid)
            .IsUnique();
    }
}