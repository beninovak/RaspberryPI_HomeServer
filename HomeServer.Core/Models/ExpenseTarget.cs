using System.Text.Json.Serialization;

namespace HomeServer.Core.Models;

public class ExpenseTarget {
    public required uint Id { get; init; }
    public required Guid Guid { get; init; } = Guid.NewGuid();
    public required string Title { get; set; }
    public required decimal Total { get; set; }
    public required ExpenseCategory ExpenseCategory { get; set; }
    
    public User? CreatedBy { get; init; }
    public required DateTime CreatedAt { get; init; }
    
    public User? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
}