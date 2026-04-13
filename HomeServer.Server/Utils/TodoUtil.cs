using HomeServer.Core;
using HomeServer.Server.Models;

namespace HomeServer.Server.Utils;

public class TodoUtil {
    public static TodoDTO TodoToDTO(Todo todo) {
        return new TodoDTO {
            Order = todo.Order,
            IsDone = todo.IsDone,
            Content = todo.Content,
            CreatedAt = todo.CreatedAt,
            TodoSectionId = todo.TodoSectionId,
        };
    }

    public static Todo DTOToTodo(TodoDTO todoDTO) {
        return new Todo {
            Order = todoDTO.Order,
            IsDone = todoDTO.IsDone,
            Content = todoDTO.Content,
            CreatedAt = todoDTO.CreatedAt,
            CreatedBy = todoDTO.CreatedBy,
            TodoSectionId = todoDTO.TodoSectionId,
        };
    }
}