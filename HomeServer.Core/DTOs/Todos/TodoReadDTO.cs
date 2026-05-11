namespace HomeServer.Core.DTOs.Todos;

public class TodoReadDTO {
    public required Guid Guid { get; init; }
    public required uint Order { get; init; }
    public required string Content { get; init; }
    public required bool IsDone { get; init; }
    public required DateTime CreatedAt { get; init; } // TODO - Am res to nucou tho? Loh bi biu sam nek info ampak ehh?
    public Guid? UpdatedBy { get; init; } // TODO - Am res to nucou tho? Loh bi biu sam nek info ampak ehh?
}