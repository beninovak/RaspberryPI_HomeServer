using System.ComponentModel.DataAnnotations;

namespace HomeServer.Models.Expenses;

public class Expense {
    public required Guid Guid { get; init; }
    public required Guid ExpenseCategoryId { get; init; }
    public required uint Order { get; init; }
    [MaxLength(50)] public required string Title { get; init; }
    [MaxLength(250)] public string? Description { get; init; }
    [MaxLength(100)] public string? ImagePath { get; init; }
    public required decimal Total { get; init; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}