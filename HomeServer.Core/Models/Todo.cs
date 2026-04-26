using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HomeServer.Core.Models;

public partial class Todo : ObservableObject {
    [ObservableProperty] private uint? todoSectionId;
    [ObservableProperty] private uint? order;
    [ObservableProperty] private string? content;
    [ObservableProperty] private bool? isDone;
    
    public uint? Id { get; init; }
    public uint? CreatedBy { get; init; }
    public DateTime? CreatedAt { get; init; }
    
    public uint? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
    //public List<SqliteBlob> Files; // Maybe sam link do files??
}