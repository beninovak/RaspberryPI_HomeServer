using HomeServer.Models.Expenses;
using HomeServer.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeServer.Database.Expenses;

public class ExpenseDBConfiguration : IEntityTypeConfiguration<Expense> {
    public void Configure(EntityTypeBuilder<Expense> builder) {
        builder
            .HasKey(expense => expense.Guid);
        
        builder
            .HasOne<ExpenseCategory>()
            .WithMany(category => category.Expenses) //  Navigation property
            .HasForeignKey(expense => expense.ExpenseCategoryId)
            .HasPrincipalKey(category => category.Guid)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(expense => expense.CreatedBy)
            .HasPrincipalKey(user => user.Guid)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(expense => expense.UpdatedBy)
            .HasPrincipalKey(user => user.Guid)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasAlternateKey(todo => new { todo.ExpenseCategoryId, todo.Order }); // Unique constraint
    }
}