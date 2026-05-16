using HomeServer.Models.Todos;
using HomeServer.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeServer.Data.Todos;

public class TodoCategoryDBConfiguration : IEntityTypeConfiguration<TodoCategory> {
    public void Configure(EntityTypeBuilder<TodoCategory> builder) {
        builder
            .HasKey(category => category.Guid);
        
        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(category => category.CreatedBy)
            .HasPrincipalKey(user => user.Guid)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(category => category.UpdatedBy)
            .HasPrincipalKey(user => user.Guid)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasAlternateKey(category => new { category.CreatedBy, category.Order }); // Unique constraint
    }
}