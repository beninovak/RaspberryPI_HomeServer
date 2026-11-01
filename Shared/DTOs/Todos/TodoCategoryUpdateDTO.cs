using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Todos;

public class TodoCategoryUpdateDTO {
    public required uint Order { get; init; }
    [MaxLength(50)] public required string Title { get; init; }
}