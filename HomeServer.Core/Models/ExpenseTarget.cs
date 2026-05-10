using System.Text.Json.Serialization;

namespace HomeServer.Core.Models;

public class ExpenseTarget {
    public required uint Id { get; init; }
    public required Guid Guid { get; init; } = Guid.NewGuid();
    public required uint ExpenseCategoryId { get; set; }

    public required string Title { get; set; }
    public required decimal Total { get; set; }
    
    public uint? CreatedBy { get; init; }
    public required DateTime CreatedAt { get; init; }
    
    public uint? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
}