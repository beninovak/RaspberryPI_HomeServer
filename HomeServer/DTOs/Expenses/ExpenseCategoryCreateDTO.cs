namespace HomeServer.DTOs.Expenses;

public class ExpenseCategoryCreateDTO {
    public required uint Order { get; init; }
    public required string Title { get; init; }
    public required string Color { get; init; }
    public required Guid CreatedBy { get; init; }
}