using System.ComponentModel.DataAnnotations;

namespace HomeServer.DTOs.Todos;

public class TodoCategoryCreateDTO {
    public required uint Order { get; init; }
    [MaxLength(50)] public required string Title { get; init; }
    public required Guid CreatedBy { get; init; }
}