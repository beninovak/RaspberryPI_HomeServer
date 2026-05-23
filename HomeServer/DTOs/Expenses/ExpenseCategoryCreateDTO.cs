using System.ComponentModel.DataAnnotations;

namespace HomeServer.DTOs.Expenses;

public class ExpenseCategoryCreateDTO {
    public required uint Order { get; init; }
    [MaxLength(50)] public required string Title { get; init; }
    [MaxLength(10)] public required string Color { get; init; }
    public required Guid CreatedBy { get; init; }
}