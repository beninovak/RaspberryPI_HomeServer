namespace HomeServer.Core.DTOs.Todos;

public class TodoCategoryReadDTO {
    public required Guid Guid { get; init; }
    public required string Title { get; init; }
    public required List<TodoReadDTO> Todos { get; init; } 
}