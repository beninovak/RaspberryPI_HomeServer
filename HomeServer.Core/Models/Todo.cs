using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Core.Models;

[Index(nameof(TodoSectionId), nameof(Order), IsUnique = true)]
public partial class Todo : ObservableObject {
    public required uint Id { get; init; }
    public required Guid Guid { get; init; } = Guid.NewGuid();
    public required uint TodoSectionId { get; init; }
    public required uint Order { get; init; }
    public required string Content { get; set; }
    public required bool IsDone { get; set; }
    
    public User? CreatedBy { get; init; }
    public required DateTime CreatedAt { get; set; }
    
    public User? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
    //public List<SqliteBlob> Files; // Maybe sam link do files??
}