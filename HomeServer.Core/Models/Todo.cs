using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Core.Models;

[Index(nameof(TodoSectionId), nameof(Order), IsUnique = true)]
public partial class Todo : ObservableObject {
    public uint Id { get; init; }
    public Guid Guid { get; init; }
    
    [JsonIgnore]
    public required uint TodoSectionId { get; set; }
    public required uint Order { get; set; }
    public required string Content { get; set; }
    public bool IsDone { get; set; }
    
    [JsonIgnore]
    public User? CreatedBy { get; init; }
    public required DateTime CreatedAt { get; set; }
    
    [JsonIgnore]
    public User? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
    //public List<SqliteBlob> Files; // Maybe sam link do files??
}