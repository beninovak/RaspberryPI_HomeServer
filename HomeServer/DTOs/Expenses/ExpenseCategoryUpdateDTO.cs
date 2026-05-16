namespace HomeServer.DTOs.Expenses;

public class ExpenseCategoryUpdateDTO {
    public required uint Order { get; init; }
    public required string Title { get; init; }
    public required string Color { get; init; }
    public Guid? UpdatedBy { get; init; }
}