using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Core.Models;

public class TodoSection {
    public uint Id { get; init; }
    public uint Order { get; init; }
    public string Title { get; set; } = string.Empty;
    
    [JsonIgnore]
    public User? CreatedBy { get; init; }
    public DateTime CreatedAt { get; set; }
    
    [JsonIgnore]
    public User? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }

    public List<Todo> Todos { get; set; } = [];
}