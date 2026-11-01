using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Todos;

public class TodoCreateDTO {
    public required Guid Guid { get; init; }
    public required Guid TodoCategoryId { get; init; }
    public required uint Order { get; init; }
    [MaxLength(50)] public required string Content { get; init; }
}