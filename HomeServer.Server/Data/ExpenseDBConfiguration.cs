using HomeServer.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeServer.Server.Data;

public class ExpenseDBConfiguration : IEntityTypeConfiguration<Expense> {
    public void Configure(EntityTypeBuilder<Expense> builder) {
        builder
            .HasOne<ExpenseCategory>()
            .WithMany(category => category.Expenses) //  Navigation property
            .HasForeignKey(expense => expense.ExpenseCategoryId)
            .HasPrincipalKey(category => category.Id)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(expense => expense.CreatedBy)
            .HasPrincipalKey(user => user.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(expense => expense.UpdatedBy)
            .HasPrincipalKey(user => user.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(todo => todo.Guid)
            .IsUnique();

        builder
            .HasAlternateKey(todo => new { todo.ExpenseCategoryId, todo.Order }); // Unique constraint
    }
}