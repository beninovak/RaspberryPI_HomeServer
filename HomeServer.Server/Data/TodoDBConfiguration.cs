using HomeServer.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeServer.Server.Data;

public class TodoDBConfiguration : IEntityTypeConfiguration<Todo> {
    public void Configure(EntityTypeBuilder<Todo> builder) {
        builder
            .HasOne<TodoCategory>()
            .WithMany(category => category.Todos) //  Navigation property
            .HasForeignKey(todo => todo.TodoCategoryId)
            .HasPrincipalKey(category => category.Id)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(todo => todo.CreatedBy)
            .HasPrincipalKey(user => user.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(todo => todo.UpdatedBy)
            .HasPrincipalKey(user => user.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(todo => todo.Guid)
            .IsUnique();
        
        builder
            .HasAlternateKey(todo => new { todo.TodoCategoryId, todo.Order }); // Unique constraint
    }
}