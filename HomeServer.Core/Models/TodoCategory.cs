using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Core.Models;

[Index(nameof(Order), IsUnique = true)]
public class TodoCategory {
    public uint Id { get; init; }
    public required Guid Guid { get; init; } = Guid.NewGuid();
    public required uint Order { get; init; }
    public required string Title { get; init; }
    
    [JsonIgnore]
    public User? CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    
    [JsonIgnore]
    public User? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }

    public List<Todo> Todos { get; init; } = [];
}