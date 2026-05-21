using HomeServer.DTOs.Todos;
using HomeServer.Exceptions;
using HomeServer.Models.Todos;
using HomeServer.Repositories;

namespace HomeServer.Services;

public class TodoService(ITodoRepository todoRepository) : ITodoService {

    #region Todos
    public async Task<List<TodoCategoryReadDTO>> GetAllAsync() {
        List<TodoCategory> dbCategories = await todoRepository.GetAllAsync();

        return dbCategories.Select(category => new TodoCategoryReadDTO {
                Guid = category.Guid,
                Title = category.Title,
                Todos = ModelToReadDTOMany(category.Todos),
            })
            .ToList();
    }

    public async Task<TodoReadDTO> GetByGuidAsync(Guid guid) {
        Todo? todo = await todoRepository.GetByGuidAsync(guid);
        if (todo is null) {
            throw new Exception("No todo");
        }
        return ModelToReadDTO(todo);
    }
    
    public async Task<Guid> CreateAsync(TodoCreateDTO todoCreateDto) {
        Todo newTodo = CreateDTOToModel(todoCreateDto);
        await todoRepository.CreateAsync(newTodo);
        return newTodo.Guid;
    }
    
    public async Task UpdateAsync(Guid guid, TodoUpdateDTO todoUpdateDto) {
        await todoRepository.UpdateAsync(UpdateDTOToModel(guid, todoUpdateDto));
    }
    
    public async Task DeleteAsync(Guid guid) {
        Todo? deleteTodo = await todoRepository.GetByGuidAsync(guid);
        if (deleteTodo is null) {
            throw new Exception("No todo");
        }
        await todoRepository.DeleteAsync(deleteTodo);
    }
    #endregion
    
    #region Categories
    public async Task<TodoCategoryReadDTO> GetCategoryByGuidAsync(Guid guid) {
        TodoCategory? category = await todoRepository.GetCategoryByGuidAsync(guid);
        if (category is null) {
            throw new Exception("No todo category");
        }
        return CategoryModelToReadDTO(category);
    }

    public async Task<Guid> CreateCategoryAsync(TodoCategoryCreateDTO todoCategoryCreateDto) {
        TodoCategory newCategory = CategoryCreateDTOToModel(todoCategoryCreateDto);
        await todoRepository.CreateCategoryAsync(newCategory);
        return newCategory.Guid;
    }
    
    public async Task UpdateCategoryAsync(Guid guid, TodoCategoryUpdateDTO todoCategoryUpdateDto) {
        await todoRepository.UpdateCategoryAsync(CategoryUpdateDTOToModel(guid, todoCategoryUpdateDto));
    }
    
    public async Task DeleteCategoryAsync(Guid guid) {
        TodoCategory? deleteCategory = await todoRepository.GetCategoryByGuidAsync(guid);
        if (deleteCategory is null) {
            throw new Exception("No todo");
        }
        await todoRepository.DeleteCategoryAsync(deleteCategory);
    }
    #endregion
    
    #region Converters
    private TodoReadDTO ModelToReadDTO(Todo todo) {
        return new TodoReadDTO {
            Guid = todo.Guid,
            Content = todo.Content,
            IsDone = todo.IsDone,
        };
    }

    private List<TodoReadDTO> ModelToReadDTOMany(List<Todo> todos) {
        List<TodoReadDTO> convertedList = [];
        foreach (Todo todo in todos) {
            convertedList.Add(ModelToReadDTO(todo));
        }
        return convertedList;
    }

    private Todo CreateDTOToModel(TodoCreateDTO todoCreateDto) {
        return new Todo {
            Guid = todoCreateDto.Guid,
            TodoCategoryId = todoCreateDto.TodoCategoryId,
            Order = todoCreateDto.Order,
            Content = todoCreateDto.Content,
            IsDone = false,
            CreatedBy = todoCreateDto.CreatedBy,
            CreatedAt = DateTime.UtcNow,
        };
    }

    private Todo UpdateDTOToModel(Guid guid, TodoUpdateDTO todoUpdateDto) {
        return new Todo {
            Guid = guid,
            TodoCategoryId = todoUpdateDto.TodoCategoryId,
            Order = todoUpdateDto.Order,
            Content = todoUpdateDto.Content,
            IsDone = todoUpdateDto.IsDone,
            UpdatedBy = todoUpdateDto.UpdatedBy,
            UpdatedAt = DateTime.UtcNow,
        };
    }

    private TodoCategoryReadDTO CategoryModelToReadDTO(TodoCategory todoCategory) {
        return new TodoCategoryReadDTO {
            Guid = todoCategory.Guid,
            Title = todoCategory.Title,
            Todos = ModelToReadDTOMany(todoCategory.Todos),
        };
    }
    
    private TodoCategory CategoryCreateDTOToModel(TodoCategoryCreateDTO todoCategoryCreateDto) {
        return new TodoCategory {
            Guid = todoCategoryCreateDto.Guid,
            Order = todoCategoryCreateDto.Order,
            Title = todoCategoryCreateDto.Title,
            CreatedBy = todoCategoryCreateDto.CreatedBy,
            CreatedAt = DateTime.UtcNow,
            Todos = [],
        };
    }
    
    private TodoCategory CategoryUpdateDTOToModel(Guid guid, TodoCategoryUpdateDTO todoCategoryUpdateDto) {
        return new TodoCategory {
            Guid = guid,
            Order = todoCategoryUpdateDto.Order,
            Title = todoCategoryUpdateDto.Title,
            UpdatedAt = DateTime.UtcNow,
            Todos = [], // TODO - poglej če to rab bit required? Maybe bol daj sam nullable pa ga kle ne passam?
        };
    }
    #endregion
}