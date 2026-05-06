using HomeServer.Core.Models;
using HomeServer.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Server.Repositories;

public class TodoRepository(MainDbContext db) : ITodoRepository {
    public async Task<List<TodoCategory>> GetAllTodos() {
        return await db.TodoCategories
            .Include(section => section.Todos)
            .OrderBy(section => section.Order)
            .ThenBy(section => section.Title)
            .ThenBy(section => section.Todos.Any() ? section.Todos.First().Order : 0)
            .ToListAsync();
    }

    public async Task<Todo?> GetTodoById(uint id) {
        return await db.Todos.FindAsync(id);
    }

    public async Task CreateTodo(Todo todo) {
        todo.CreatedAt = DateTime.Now;
        db.Todos.Add(todo);
        await db.SaveChangesAsync();
    }

    public async Task UpdateTodo(Todo todo) {
        db.Todos.Update(todo);
        await db.SaveChangesAsync();
    }

    public async Task CreateTodoSection(TodoCategory todoCategory) {
        todoCategory.CreatedAt = DateTime.Now;
        db.TodoCategories.Add(todoCategory);
        await db.SaveChangesAsync();
    }

    public async Task UpdateTodoSection(TodoCategory todoCategory) {
        db.TodoCategories.Update(todoCategory);
        await db.SaveChangesAsync();
    }
}