using HomeServer.Core.Models.Expenses;
using HomeServer.Core.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeServer.Server.Data.Expenses;

public class ExpenseTargetDBConfiguration : IEntityTypeConfiguration<ExpenseTarget> {
    public void Configure(EntityTypeBuilder<ExpenseTarget> builder) {
        builder
            .HasKey(target => target.Guid);
        
        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(target => target.CreatedBy)
            .HasPrincipalKey(user => user.Guid)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(target => target.UpdatedBy)
            .HasPrincipalKey(user => user.Guid)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne<ExpenseCategory>()
            .WithMany()
            .HasForeignKey(target => target.ExpenseCategoryId)
            .HasPrincipalKey(category => category.Guid);
    }
}