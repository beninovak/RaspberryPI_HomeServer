namespace HomeServer.Core.DTOs.Todos;

public class TodoCategoryUpdateDTO {
    public required uint Order { get; init; }
    public required string Title { get; init; }
    public required Guid UpdatedBy { get; init; }
}