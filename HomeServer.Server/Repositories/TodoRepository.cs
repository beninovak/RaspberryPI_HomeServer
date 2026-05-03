using HomeServer.Core.Models;
using HomeServer.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Server.Repositories;

public class TodoRepository(MainDbContext db) : ITodoRepository {
    public async Task<List<TodoSection>> GetAllTodos() {
        return await db.TodoSections
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

    public async Task CreateTodoSection(TodoSection todoSection) {
        todoSection.CreatedAt = DateTime.Now;
    
        db.TodoSections.Add(todoSection);
        await db.SaveChangesAsync();
    }

    public async Task UpdateTodoSection(TodoSection todoSection) {
        db.TodoSections.Update(todoSection);
        await db.SaveChangesAsync();
    }
}