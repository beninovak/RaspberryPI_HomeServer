using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Todos;

public class TodoReadDTO {
    public required Guid Guid { get; init; }
    [MaxLength(250)] public required string Content { get; init; }
    public required bool IsDone { get; init; }
}