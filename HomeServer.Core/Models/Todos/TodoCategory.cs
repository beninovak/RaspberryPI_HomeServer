namespace HomeServer.Core.Models.Todos;

public class TodoCategory {
    public required Guid Guid { get; init; }
    public required uint Order { get; init; }
    public required string Title { get; init; }
    public Guid CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public Guid? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public required List<Todo> Todos { get; init; }
}