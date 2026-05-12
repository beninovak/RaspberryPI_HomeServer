namespace HomeServer.Core.Models.Expenses;

public class ExpenseTargetHistory {
    public required Guid Guid { get; init; }
    public required Guid ExpenseTargetId { get; init; }
    public required uint Month { get; init; }
    public required uint Year { get; init; }
    public required bool TargetHit { get; init; }
    public string? Description { get; init; }
}