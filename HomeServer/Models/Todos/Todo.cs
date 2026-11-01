using System.ComponentModel.DataAnnotations;

namespace HomeServer.Models.Todos;

public class Todo {
    public required Guid Guid { get; init; }
    public required Guid TodoCategoryId { get; init; }
    public required uint Order { get; init; }
    [MaxLength(50)] public required string Content { get; init; }
    public required bool IsDone { get; init; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    //public List<SqliteBlob> Files; // Maybe sam link do files??
}