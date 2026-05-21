using HomeServer.Models.Todos;
using HomeServer.Data;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Repositories;

public class TodoRepository(MainDbContext db) : ITodoRepository {
    
    #region Todos
    public async Task<List<TodoCategory>> GetAllAsync() {
        return await db.TodoCategories
            .Include(category => category.Todos)
            .OrderBy(category => category.Order)
            .ThenBy(category => category.Title)
            .ThenBy(category => category.Todos.Any() ? category.Todos.First().Order : 0) // TODO - uporab FirstOrDefault? Kva če jih nima nč?
            .ToListAsync();
    }

    public async Task<Todo?> GetByGuidAsync(Guid guid) {
        return await db.Todos.FindAsync(guid);
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
    public async Task<TodoCategory?> GetCategoryByGuidAsync(Guid guid) {
        return await db.TodoCategories.FindAsync(guid);
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