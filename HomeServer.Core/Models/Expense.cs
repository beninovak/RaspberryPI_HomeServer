using System.Drawing;
using System.Text.Json.Serialization;

namespace HomeServer.Core.Models;

public class Expense {
    public required uint Id { get; init; }
    public required Guid Guid { get; init; } = Guid.NewGuid();
    public required uint ExpenseCategoryId { get; set; }

    public required uint Order { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? ImagePath { get; set; }
    public required decimal Total { get; set; }
    
    public uint? CreatedBy { get; init; }
    public required DateTime CreatedAt { get; set; }
    
    public uint? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
}