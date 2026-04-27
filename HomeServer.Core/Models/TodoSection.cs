namespace HomeServer.Core.Models;

public class TodoSection {
    public uint Id { get; init; }
    public uint Order { get; init; }
    public string Title { get; set; } = string.Empty;
    
    public uint CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public uint? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }

    public List<Todo> Todos { get; set; } = [];
}