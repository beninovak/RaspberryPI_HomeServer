using HomeServer.Models.Expenses;
using HomeServer.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeServer.Database.Expenses;

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
            .WithOne(category => category.Target)
            .HasForeignKey<ExpenseTarget>(target => target.ExpenseCategoryId)
            .HasPrincipalKey<ExpenseCategory>(category => category.Guid);
    }
}