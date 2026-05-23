using System.ComponentModel.DataAnnotations;

namespace HomeServer.Models.Expenses;

public class ExpenseTargetHistory {
    public required Guid Guid { get; init; }
    public required Guid ExpenseTargetId { get; init; }
    public required uint Month { get; init; }
    public required uint Year { get; init; }
    public required bool TargetHit { get; init; }
    [MaxLength(250)] public string? Description { get; init; }
}