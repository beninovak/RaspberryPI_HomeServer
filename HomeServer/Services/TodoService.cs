using HomeServer.DTOs.Todos;
using HomeServer.Exceptions;
using HomeServer.Models.Todos;
using HomeServer.Repositories;

namespace HomeServer.Services;

public class TodoService(ITodoRepository todoRepository) : ITodoService {

    #region Todos
    public async Task<IReadOnlyCollection<TodoCategoryReadDTO>> GetAllAsync() {
        IReadOnlyCollection<TodoCategory> dbCategories = await todoRepository.GetAllAsync();

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
    
    public async Task<Guid> CreateAsync(TodoCreateDTO createDto) {
        Todo newTodo = CreateDTOToModel(createDto);
        await todoRepository.CreateAsync(newTodo);
        return newTodo.Guid;
    }
    
    public async Task UpdateAsync(Guid guid, TodoUpdateDTO updateDto) {
        await todoRepository.UpdateAsync(UpdateDTOToModel(guid, updateDto));
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

    public async Task<Guid> CreateCategoryAsync(TodoCategoryCreateDTO categoryCreateDto) {
        TodoCategory newCategory = CategoryCreateDTOToModel(categoryCreateDto);
        await todoRepository.CreateCategoryAsync(newCategory);
        return newCategory.Guid;
    }
    
    public async Task UpdateCategoryAsync(Guid guid, TodoCategoryUpdateDTO categoryUpdateDto) {
        await todoRepository.UpdateCategoryAsync(CategoryUpdateDTOToModel(guid, categoryUpdateDto));
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

    private Todo CreateDTOToModel(TodoCreateDTO createDto) {
        return new Todo {
            Guid = createDto.Guid,
            TodoCategoryId = createDto.TodoCategoryId,
            Order = createDto.Order,
            Content = createDto.Content,
            IsDone = false,
            CreatedBy = createDto.CreatedBy,
            CreatedAt = DateTime.UtcNow,
        };
    }

    private Todo UpdateDTOToModel(Guid guid, TodoUpdateDTO updateDto) {
        return new Todo {
            Guid = guid,
            TodoCategoryId = updateDto.TodoCategoryId,
            Order = updateDto.Order,
            Content = updateDto.Content,
            IsDone = updateDto.IsDone,
            UpdatedBy = updateDto.UpdatedBy,
            UpdatedAt = DateTime.UtcNow,
        };
    }

    private TodoCategoryReadDTO CategoryModelToReadDTO(TodoCategory category) {
        return new TodoCategoryReadDTO {
            Guid = category.Guid,
            Title = category.Title,
            Todos = ModelToReadDTOMany(category.Todos),
        };
    }
    
    private TodoCategory CategoryCreateDTOToModel(TodoCategoryCreateDTO categoryCreateDto) {
        return new TodoCategory {
            Guid = Guid.NewGuid(),
            Order = categoryCreateDto.Order,
            Title = categoryCreateDto.Title,
            CreatedBy = categoryCreateDto.CreatedBy,
            CreatedAt = DateTime.UtcNow,
            Todos = [],
        };
    }

    private static TodoCategory CategoryUpdateDTOToModel(Guid guid, TodoCategoryUpdateDTO categoryUpdateDto) {
        return new TodoCategory {
            Guid = guid,
            Order = categoryUpdateDto.Order,
            Title = categoryUpdateDto.Title,
            UpdatedBy = categoryUpdateDto.UpdatedBy,
            UpdatedAt = DateTime.UtcNow,
            Todos = [], // TODO - poglej če to rab bit required? Maybe bol daj sam nullable pa ga kle ne passam?
        };
    }
    #endregion
}