namespace Shared.DTOs.Todos;

public class TodoCategoryReadDTO {
    public required Guid Guid { get; init; }
    public required string Title { get; init; }
    public required IReadOnlyCollection<TodoReadDTO> Todos { get; init; } 
}