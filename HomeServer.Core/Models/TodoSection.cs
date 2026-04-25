using System.ComponentModel.DataAnnotations;

namespace HomeServer.Core.Models;

public class TodoSection {
    public uint Id { get; init; }
    public uint Order { get; init; }

    [MaxLength(50)] public string Title { get; init; } = string.Empty;

    public DateTime CreatedAt { get; init; } = DateTime.Now;

    public List<Todo> Todos { get; set; } = [];
}