using HomeServer.Core.DTOModelConverters;
using HomeServer.Core.DTOs.Todos;
using HomeServer.Core.Models.Todos;
using HomeServer.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Server.Repositories;

public class TodoRepository(MainDbContext db, ITodoConverter todoConverter) : ITodoRepository {
    
    #region Todos
    public async Task<List<TodoCategoryReadDTO>> GetAllTodosAsync() {
        List<TodoCategory> todoCategories = await db.TodoCategories
            .Include(category => category.Todos)
            .OrderBy(category => category.Order)
            .ThenBy(category => category.Title)
            .ThenBy(category => category.Todos.Any() ? category.Todos.First().Order : 0)
            .ToListAsync();

        List<TodoCategoryReadDTO> todoCategoryReadDTOs = [];
        todoCategoryReadDTOs.AddRange(
        todoCategories.Select(category => new TodoCategoryReadDTO {
            Guid = category.Guid,
            Title = category.Title,
            Todos = todoConverter.TodoModelToReadDTOMany(category.Todos),
        }));

        return todoCategoryReadDTOs;
    }

    public async Task<TodoReadDTO?> GetTodoByGuidAsync(Guid guid) {
        Todo? todo = await db.Todos.FirstOrDefaultAsync(todo => todo.Guid.Equals(guid));
        return todo is null ? null : todoConverter.TodoModelToReadDTO(todo);
    }

    public async Task CreateTodoAsync(TodoCreateDTO todoCreateDto) {
        Todo todo = todoConverter.TodoCreateDTOToModel(todoCreateDto);
        db.Todos.Add(todo);
        await db.SaveChangesAsync();
    }

    public async Task<bool> UpdateTodoAsync(Guid guid, TodoUpdateDTO TodoUpdateDto) {
        if (await GetTodoByGuidAsync(guid) is null) {
            return false;
        }
        
        Todo todo = todoConverter.TodoUpdateDTOToModel(guid, TodoUpdateDto);
        db.Todos.Update(todo);
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteTodoAsync(Guid guid) {
        Todo? todo = await db.Todos.FirstOrDefaultAsync(todo => todo.Guid.Equals(guid));
        if (todo is null) {
            return false;
        }
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return true;
    }

    #endregion

    #region Categories
    public async Task CreateTodoCategoryAsync(TodoCategoryCreateDTO todoCategoryCreateDto) {
        TodoCategory todoCategory = todoConverter.TodoCategoryCreateDTOToModel(todoCategoryCreateDto);
        db.TodoCategories.Add(todoCategory);
        await db.SaveChangesAsync();
    }

    public async Task UpdateTodoCategoryAsync(Guid guid, TodoCategoryUpdateDTO todoCategoryUpdateDto) {
        TodoCategory todoCategory = todoConverter.TodoCategoryUpdateDTOToModel(guid, todoCategoryUpdateDto);
        db.TodoCategories.Update(todoCategory);
        await db.SaveChangesAsync();
    }

    public async Task<bool> DeleteTodoCategoryAsync(Guid guid) {
        TodoCategory? category = await db.TodoCategories.FirstOrDefaultAsync(category => category.Guid.Equals(guid));
        if (category is null) {
            return false;
        }
        db.TodoCategories.Remove(category);
        await db.SaveChangesAsync();
        return true;
    }
    #endregion
}