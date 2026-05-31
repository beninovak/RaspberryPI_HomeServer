using System.ComponentModel.DataAnnotations;

namespace HomeServer.DTOs.Expenses;

public class ExpenseTargetCreateDTO {
    public required Guid ExpenseCategoryId { get; init; }
    [MaxLength(50)] public required string Title { get; init; }
    [MaxLength(250)] public string? Description { get; init; }
    public required decimal Total { get; init; }
}