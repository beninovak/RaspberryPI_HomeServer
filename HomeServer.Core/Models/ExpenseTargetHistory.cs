namespace HomeServer.Core.Models;

public class ExpenseTargetHistory {
    public required uint Id { get; init; }
    public required Guid Guid { get; init; } = Guid.NewGuid();
    public required uint ExpenseTargetId { get; init; }
    public required uint Month { get; init; }
    public required uint Year { get; init; }
    public required bool TargetHit { get; init; }
    
    public string? Description { get; init; }
}