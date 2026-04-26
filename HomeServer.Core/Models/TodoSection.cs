using System.ComponentModel.DataAnnotations;

namespace HomeServer.Core.Models;

public class TodoSection {
    public uint Id { get; init; }
    public uint Order { get; init; }
    public string Title { get; init; } = string.Empty;
    
    public uint? CreatedBy { get; init; }
    public DateTime? CreatedAt { get; init; }
    
    public uint? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }

    public List<Todo> Todos { get; set; } = [];
}