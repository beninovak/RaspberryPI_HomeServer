using System.ComponentModel.DataAnnotations;

namespace HomeServer.Models.Expenses;

public class ExpenseTarget {
    public required Guid Guid { get; init; }
    public required Guid ExpenseCategoryId { get; init; }
    [MaxLength(50)] public required string Title { get; init; }
    [MaxLength(250)] public string? Description { get; init; }
    public required decimal Total { get; init; }
    public Guid CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public Guid? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
}