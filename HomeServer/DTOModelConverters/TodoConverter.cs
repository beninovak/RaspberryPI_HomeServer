using HomeServer.DTOs.Todos;
using HomeServer.Models.Todos;

namespace HomeServer.DTOModelConverters;

public class TodoConverter : ITodoConverter {
    
    #region Todos
    
    public TodoReadDTO TodoModelToReadDTO(Todo todo) {
        return new TodoReadDTO {
            Guid = todo.Guid,
            Content = todo.Content,
            IsDone = todo.IsDone,
        };
    }

    public List<TodoReadDTO> TodoModelToReadDTOMany(List<Todo> todos) {
        List<TodoReadDTO> convertedList = [];
        foreach (Todo todo in todos) {
            convertedList.Add(TodoModelToReadDTO(todo));
        }
        return convertedList;
    }

    public Todo TodoCreateDTOToModel(TodoCreateDTO todoCreateDto) {
        return new Todo {
            Guid = Guid.NewGuid(),
            TodoCategoryId = todoCreateDto.TodoCategoryId,
            Order = todoCreateDto.Order,
            Content = todoCreateDto.Content,
            IsDone = false,
            CreatedBy = todoCreateDto.CreatedBy,
            CreatedAt = DateTime.UtcNow,
        };
    }

    public Todo TodoUpdateDTOToModel(Guid guid, TodoUpdateDTO todoUpdateDto) {
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

    #endregion
    
    #region Categories
    public TodoCategory TodoCategoryCreateDTOToModel(TodoCategoryCreateDTO todoCategoryCreateDto) {
        return new TodoCategory {
            Guid = Guid.NewGuid(),
            Order = todoCategoryCreateDto.Order,
            Title = todoCategoryCreateDto.Title,
            CreatedBy = todoCategoryCreateDto.CreatedBy,
            CreatedAt = DateTime.UtcNow,
            Todos = [],
        };
    }
    public TodoCategory TodoCategoryUpdateDTOToModel(Guid guid, TodoCategoryUpdateDTO todoCategoryUpdateDto) {
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