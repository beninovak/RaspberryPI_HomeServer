using System.Drawing;
using System.Text.Json.Serialization;

namespace HomeServer.Core.Models;

public class ExpenseCategory {
    public required uint Id { get; init; }
    public required Guid Guid { get; init; } = Guid.NewGuid();
    public required uint Order { get; set; }
    public required string Title { get; set; }
    public required string Color { get; set; }
    
    public uint? CreatedBy { get; init; }
    public required DateTime CreatedAt { get; set; }
    
    public uint? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
    
    public List<Expense> Expenses { get; init; } = [];
}