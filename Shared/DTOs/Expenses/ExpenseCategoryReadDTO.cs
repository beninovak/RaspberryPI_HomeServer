namespace Shared.DTOs.Expenses;

public class ExpenseCategoryReadDTO {
    public required Guid Guid { get; init; }
    public required string Title { get; init; }
    public required string Color { get; init; }
    public required IReadOnlyCollection<ExpenseReadDTO> Expenses { get; init; } 
    public ExpenseTargetReadDTO? Target { get; init; }
}