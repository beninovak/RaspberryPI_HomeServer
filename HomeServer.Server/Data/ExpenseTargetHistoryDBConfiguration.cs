using HomeServer.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeServer.Server.Data;

public class ExpenseTargetHistoryDBConfiguration : IEntityTypeConfiguration<ExpenseTargetHistory> {
    public void Configure(EntityTypeBuilder<ExpenseTargetHistory> builder) {
        builder
            .HasOne<ExpenseTarget>()
            .WithMany()
            .HasForeignKey(history => history.ExpenseTargetId)
            .HasPrincipalKey(target => target.Id);

        builder
            .HasAlternateKey(history => new { history.ExpenseTargetId, history.Month, history.Year });
        
        builder
            .HasIndex(history => history.Guid)
            .IsUnique();
    }
}