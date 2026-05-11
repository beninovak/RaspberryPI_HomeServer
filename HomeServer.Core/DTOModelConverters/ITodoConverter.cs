using HomeServer.Core.DTOs.Todos;
using HomeServer.Core.Models.Todos;

namespace HomeServer.Core.DTOModelConverters;

public interface ITodoConverter {
    public TodoReadDTO TodoModelToReadDTO(Todo todo);
    public List<TodoReadDTO> TodoModelToReadDTOMany(List<Todo> todos);

    public Todo TodoCreateDTOToModel(TodoCreateDTO todoCreateDto);
    public Todo TodoUpdateDTOToModel(Guid guid, TodoUpdateDTO todoUpdateDto);
    
    public TodoCategory TodoCategoryCreateDTOToModel(TodoCategoryCreateDTO todoCategoryCreateDto);
    public TodoCategory TodoCategoryUpdateDTOToModel(Guid guid, TodoCategoryUpdateDTO todoCategoryUpdateDto);
}
