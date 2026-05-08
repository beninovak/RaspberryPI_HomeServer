using HomeServer.Core.DTOs;
using HomeServer.Core.Models;

namespace HomeServer.Core.DTOModelConverters;

public class TodoConverter : ITodoConverter {
    public TodoCategory TodoCategoryDTOToModel(TodoCategoryDTO categoryDto) {
        return new TodoCategory {
            Guid = categoryDto.Guid,
            Order = categoryDto.Order,
            Title = categoryDto.Title,
            //CreatedBy = categoryDto.CreatedBy,
            //UpdatedBy = categoryDto.UpdatedBy,
        };
    }
    
    public Todo TodoDTOToModel(TodoDTO todoDto) {
        return new Todo {
            Guid = todoDto.Guid,
            TodoCategoryId = todoDto.TodoCategoryId,
            Order = todoDto.Order,
            Content = todoDto.Content,
            IsDone = todoDto.IsDone,
        };
    }
    
    public TodoDTO TodoModelToDto(Todo todo) {
        return new TodoDTO {
            Guid = todo.Guid,
            TodoCategoryId = todo.TodoCategoryId,
            Order = todo.Order,
            Content = todo.Content,
            IsDone = todo.IsDone,
        };
    }

    public List<TodoDTO> TodoModelToDTOMany(List<Todo> todos) {
        List<TodoDTO> convertedList = [];
        convertedList.AddRange(todos.Select(todo => new TodoDTO {
            Guid = todo.Guid,
            TodoCategoryId = todo.TodoCategoryId,
            Order = todo.Order,
            Content = todo.Content,
            IsDone = todo.IsDone,
        }));

        return convertedList;
    }
}