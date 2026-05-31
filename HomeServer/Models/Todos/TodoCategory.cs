using System.ComponentModel.DataAnnotations;

namespace HomeServer.Models.Todos;

public class TodoCategory {
    public required Guid Guid { get; init; }
    public required uint Order { get; init; }
    [MaxLength(50)] public required string Title { get; init; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public required List<Todo> Todos { get; init; }
}