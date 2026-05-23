using System.ComponentModel.DataAnnotations;

namespace HomeServer.Models.Expenses;

public class ExpenseCategory {
    public required Guid Guid { get; init; }
    public required uint Order { get; init; }
    [MaxLength(50)] public required string Title { get; init; }
    [MaxLength(10)] public required string Color { get; init; }
    public Guid CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public Guid? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public List<Expense> Expenses { get; init; } = [];
    public ExpenseTarget? Target { get; init; }
}