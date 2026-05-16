namespace HomeServer.DTOs.Todos;

public class TodoCategoryCreateDTO {
    public required uint Order { get; init; }
    public required string Title { get; init; }
    public required Guid CreatedBy { get; init; }
}