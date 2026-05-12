namespace HomeServer.Core.Models.Expenses;

public class Expense {
    public required Guid Guid { get; init; }
    public required Guid ExpenseCategoryId { get; init; }
    public required uint Order { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public string? ImagePath { get; init; }
    public required decimal Total { get; init; }
    public Guid CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public Guid? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
}