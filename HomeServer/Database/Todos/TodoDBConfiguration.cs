using HomeServer.Models.Todos;
using HomeServer.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeServer.Data.Todos;

public class TodoDBConfiguration : IEntityTypeConfiguration<Todo> {
    public void Configure(EntityTypeBuilder<Todo> builder) {
        builder
            .HasKey(todo => todo.Guid);
        
        builder
            .HasOne<TodoCategory>()
            .WithMany(category => category.Todos) //  Navigation property
            .HasForeignKey(todo => todo.TodoCategoryId)
            .HasPrincipalKey(category => category.Guid)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(todo => todo.CreatedBy)
            .HasPrincipalKey(user => user.Guid)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(todo => todo.UpdatedBy)
            .HasPrincipalKey(user => user.Guid)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasAlternateKey(todo => new { todo.TodoCategoryId, todo.Order }); // Unique constraint
    }
}