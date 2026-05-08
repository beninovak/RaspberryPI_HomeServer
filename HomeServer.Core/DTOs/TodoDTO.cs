namespace HomeServer.Core.DTOs;

public class TodoDTO {
    public required Guid Guid { get; init; }
    public required uint TodoCategoryId { get; init; }
    public required uint Order { get; init; }
    public required string Content { get; set; }
    public required bool IsDone { get; set; }
}