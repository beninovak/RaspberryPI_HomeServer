namespace HomeServer.DTOs.Expenses;

public class ExpenseCreateDTO {
    public required Guid ExpenseCategoryId { get; init; }
    public required uint Order { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public string? ImagePath { get; init; }
    public required decimal Total { get; init; }
    public required Guid CreatedBy { get; init; }
}