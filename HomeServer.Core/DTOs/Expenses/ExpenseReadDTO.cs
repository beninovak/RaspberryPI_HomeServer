namespace HomeServer.Core.DTOs.Expenses;

public class ExpenseReadDTO {
    public required Guid Guid { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public string? ImagePath { get; init; }
    public required decimal Total { get; init; }
}