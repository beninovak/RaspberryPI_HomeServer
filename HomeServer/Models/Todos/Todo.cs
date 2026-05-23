using System.ComponentModel.DataAnnotations;

namespace HomeServer.Models.Todos;

public partial class Todo {
    public required Guid Guid { get; init; }
    public required Guid TodoCategoryId { get; init; }
    public required uint Order { get; init; }
    [MaxLength(50)] public required string Content { get; init; }
    public required bool IsDone { get; init; }
    public Guid CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public Guid? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
    //public List<SqliteBlob> Files; // Maybe sam link do files??
}