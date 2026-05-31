using System.ComponentModel.DataAnnotations;

namespace HomeServer.Models.Expenses;

public class ExpenseTarget {
    public required Guid Guid { get; init; }
    public required Guid ExpenseCategoryId { get; init; }
    [MaxLength(50)] public required string Title { get; init; }
    [MaxLength(250)] public string? Description { get; init; }
    public required decimal Total { get; init; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}