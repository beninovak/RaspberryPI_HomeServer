namespace HomeServer.DTOs.Expenses;

public class ExpenseTargetReadDTO {
    public required Guid Guid { get; init; }
    // public required Guid ExpenseCategoryId { get; init; } // TODO - Sam string z title? TBD - odvisn ko bo zgledal na FE...
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required decimal Total { get; init; }
}