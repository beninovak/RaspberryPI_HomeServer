namespace HomeServer.Core.DTOs.Expenses;

public class ExpenseCategoryReadDTO {
    public required Guid Guid { get; init; }
    public required string Title { get; init; }
    public required string Color { get; init; }
    public required List<ExpenseReadDTO> Expenses { get; init; } 
    public required ExpenseTargetReadDTO Target { get; init; }
}