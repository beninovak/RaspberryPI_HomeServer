using HomeServer.Models.Todos;
using HomeServer.Data;
using HomeServer.Services;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Repositories;

public class TodoRepository(MainDbContext db) : ITodoRepository {
    
    #region Todos
    public async Task<IReadOnlyCollection<TodoCategory>> GetAllAsync(Guid userGuid) {
        return await db.TodoCategories
            .Include(category => category.Todos)
            .Where(category => category.CreatedBy.Equals(userGuid))
            .OrderBy(category => category.Order)
            .ThenBy(category => category.Title)
            .ThenBy(category => category.Todos.Any() ? category.Todos.First().Order : 0) // TODO - uporab FirstOrDefault? Kva če jih nima nč?
            .ToListAsync();
    }

    public async Task<Todo?> GetByGuidAsync(Guid guid, Guid userGuid) {
        return await db.Todos.FirstAsync(todo => todo.Guid.Equals(guid) && todo.CreatedBy.Equals(userGuid));
    }

    public async Task CreateAsync(Todo todo) {
        db.Todos.Add(todo);
        await db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Todo todo) {
        db.Todos.Update(todo);
        await db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Todo todo) {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
    }
    #endregion

    #region Categories
    public async Task<TodoCategory?> GetCategoryByGuidAsync(Guid guid, Guid userGuid) {
        return await db.TodoCategories.FirstAsync(category => category.Guid.Equals(guid) && category.CreatedBy.Equals(userGuid));
    }
    
    public async Task CreateCategoryAsync(TodoCategory todoCategory) {
        db.TodoCategories.Add(todoCategory);
        await db.SaveChangesAsync();
    }

    public async Task UpdateCategoryAsync(TodoCategory todoCategory) {
        db.TodoCategories.Update(todoCategory);
        await db.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(TodoCategory todoCategory) {
        db.TodoCategories.Remove(todoCategory);
        await db.SaveChangesAsync();
    }
    #endregion
}