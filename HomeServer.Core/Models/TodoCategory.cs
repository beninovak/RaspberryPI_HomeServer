using System.Text.Json.Serialization;

namespace HomeServer.Core.Models;

public class TodoCategory {
    public required uint Id { get; init; }
    public required Guid Guid { get; init; } = Guid.NewGuid();
    public required uint Order { get; init; }
    public required string Title { get; init; }
    
    [JsonIgnore]
    public User? CreatedBy { get; init; }
    public DateTime CreatedAt { get; set; }
    
    [JsonIgnore]
    public User? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }

    public List<Todo> Todos { get; init; } = [];
}