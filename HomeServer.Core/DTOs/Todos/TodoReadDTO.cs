namespace HomeServer.Core.DTOs.Todos;

public class TodoReadDTO {
    public required Guid Guid { get; init; }
    public required string Content { get; init; }
    public required bool IsDone { get; init; }
}