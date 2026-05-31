using System.ComponentModel.DataAnnotations;

namespace HomeServer.DTOs.Todos;

public class TodoUpdateDTO {
    public required Guid guid { get; init; }
    public required Guid TodoCategoryId { get; init; }
    public required uint Order { get; init; }
    [MaxLength(50)] public required string Content { get; init; }
    public required bool IsDone { get; init; }
}