namespace HomeServer.DTOs.Todos;

public class TodoCreateDTO {
    public required Guid TodoCategoryId { get; init; }
    public required uint Order { get; init; }
    public required string Content { get; init; }
    public required Guid CreatedBy { get; init; }
}