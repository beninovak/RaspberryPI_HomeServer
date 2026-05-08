using HomeServer.Core.DTOModelConverters;
using HomeServer.Core.DTOs;
using HomeServer.Core.Models;
using HomeServer.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Server.Repositories;

public class TodoRepository(MainDbContext db, ITodoConverter todoConverter) : ITodoRepository {
    public async Task<List<TodoByCategoryDTO>> GetAllTodos() {
        List<TodoCategory> todoCategories = await db.TodoCategories
            .Include(category => category.Todos)
            .OrderBy(category => category.Order)
            .ThenBy(category => category.Title)
            .ThenBy(category => category.Todos.Any() ? category.Todos.First().Order : 0)
            .ToListAsync();

        List<TodoByCategoryDTO> todoCategoriesDTO = [];
        todoCategoriesDTO.AddRange(
        todoCategories.Select(category => new TodoByCategoryDTO {
            Title = category.Title, 
            Todos = todoConverter.TodoModelToDTOMany(category.Todos),
        }));

        return todoCategoriesDTO;
    }

    public async Task<TodoDTO?> GetTodoById(uint id) {
        Todo? todo = await db.Todos.FindAsync(id);
        return todo is null ? null : todoConverter.TodoModelToDto(todo);
    }

    public async Task CreateTodo(TodoDTO todoDto) {
        Todo todo = todoConverter.TodoDTOToModel(todoDto);
        db.Todos.Add(todo);
        await db.SaveChangesAsync();
    }

    public async Task UpdateTodo(TodoDTO todoDto) {
        Todo todo = todoConverter.TodoDTOToModel(todoDto);
        todo.UpdatedAt = DateTime.UtcNow;
        db.Todos.Update(todo);
        await db.SaveChangesAsync();
    }

    public async Task CreateTodoCategory(TodoCategoryDTO todoCategoryDto) {
        TodoCategory todoCategory = todoConverter.TodoCategoryDTOToModel(todoCategoryDto);
        db.TodoCategories.Add(todoCategory);
        await db.SaveChangesAsync();
    }

    public async Task UpdateTodoCategory(TodoCategoryDTO todoCategoryDto) {
        TodoCategory todoCategory = todoConverter.TodoCategoryDTOToModel(todoCategoryDto);
        db.TodoCategories.Update(todoCategory);
        await db.SaveChangesAsync();
    }
}