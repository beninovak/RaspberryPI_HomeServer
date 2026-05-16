namespace HomeServer.DTOs.Expenses;

public class ExpenseTargetHistoryReadDTO {
    // public required Guid ExpenseTargetId { get; init; } // TODO - Sam string z title? TBD - odvisn ko bo zgledal na FE...
    public required uint Month { get; init; }
    public required uint Year { get; init; }
    public required bool TargetHit { get; init; }
    public string? Description { get; init; }
}