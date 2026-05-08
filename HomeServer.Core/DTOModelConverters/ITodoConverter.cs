using HomeServer.Core.DTOs;
using HomeServer.Core.Models;

namespace HomeServer.Core.DTOModelConverters;

public interface ITodoConverter {
    public TodoCategory TodoCategoryDTOToModel(TodoCategoryDTO categoryDto);
    public Todo TodoDTOToModel(TodoDTO todoDto);
    public TodoDTO TodoModelToDto(Todo todo);
    public List<TodoDTO> TodoModelToDTOMany(List<Todo> todos);
}