namespace HomeServer.Core.Models.Expenses;

public class ExpenseCategory {
    public required Guid Guid { get; init; }
    public required uint Order { get; init; }
    public required string Title { get; init; }
    public required string Color { get; init; }
    public Guid CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public Guid? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public List<Expense> Expenses { get; init; } = [];
    public ExpenseTarget? Target { get; init; } = null;
}