using HomeServer.Models.Expenses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeServer.Data.Expenses;

public class ExpenseTargetHistoryDBConfiguration : IEntityTypeConfiguration<ExpenseTargetHistory> {
    public void Configure(EntityTypeBuilder<ExpenseTargetHistory> builder) {
        builder
            .HasKey(history => history.Guid);
        
        builder
            .HasOne<ExpenseTarget>()
            .WithMany()
            .HasForeignKey(history => history.ExpenseTargetId)
            .HasPrincipalKey(target => target.Guid);

        builder
            .HasAlternateKey(history => new { history.ExpenseTargetId, history.Month, history.Year });
    }
}