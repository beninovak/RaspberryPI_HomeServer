using System.ComponentModel.DataAnnotations;

namespace HomeServer.Models.Expenses;

public class ExpenseCategory {
    public required Guid Guid { get; init; }
    public required uint Order { get; init; }
    [MaxLength(50)] public required string Title { get; init; }
    [MaxLength(10)] public required string Color { get; init; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<Expense> Expenses { get; init; } = [];
    public ExpenseTarget? Target { get; init; }
}