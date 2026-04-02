using HomeServer.Core;
using HomeServer.Server.Models;
namespace HomeServer.Server.Utils;

public class TodoUtil {
    public static TodoDTO TodoToDTO(Todo user) {
        return new TodoDTO {
            Id = user.Id,
            Order = user.Order,
            IsDone = user.IsDone,
            Content = user.Content,
            CreatedAt = user.CreatedAt,
        };
    }
    
    public static Todo DTOToTodo(TodoDTO todoDTO) {
        return new Todo {
            Id = todoDTO.Id,
            Order = todoDTO.Order,
            IsDone = todoDTO.IsDone,
            Content = todoDTO.Content,
            CreatedAt = todoDTO.CreatedAt,
        };
    }
}