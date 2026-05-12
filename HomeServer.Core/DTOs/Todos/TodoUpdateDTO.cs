namespace HomeServer.Core.DTOs.Todos;

public class TodoUpdateDTO {
    public required Guid TodoCategoryId { get; init; }
    public required uint Order { get; init; }
    public required string Content { get; init; }
    public required bool IsDone { get; init; }
    public required Guid UpdatedBy { get; init; }
}