namespace HomeServer.Core.DTOs.Expenses;

public class ExpenseTargetCreateDTO {
    public required Guid ExpenseCategoryId { get; init; }
    public required string Title { get; init; }
    public required decimal Total { get; init; }
    public required Guid CreatedBy { get; init; }
}