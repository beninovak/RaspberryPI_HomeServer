using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Core.Models;

[Index(nameof(TodoCategoryId), nameof(Order), IsUnique = true)]
public partial class Todo : ObservableObject {
    public uint Id { get; init; }
    public required Guid Guid { get; init; } = Guid.NewGuid();
    public required uint TodoCategoryId { get; init; }
    public required uint Order { get; init; }
    public required string Content { get; set; }
    public required bool IsDone { get; set; }
    
    public User? CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    
    public User? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; set; }
    //public List<SqliteBlob> Files; // Maybe sam link do files??
}