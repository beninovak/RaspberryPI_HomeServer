namespace HomeServer.Core.Models.Expenses;

public class ExpenseTarget {
    public required Guid Guid { get; init; }
    public required Guid ExpenseCategoryId { get; init; }

    public required string Title { get; init; }
    public required decimal Total { get; init; }
    public required Guid CreatedBy { get; init; }
    public required DateTime CreatedAt { get; init; }
    public Guid? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
}