using CommunityToolkit.Mvvm.ComponentModel;

namespace HomeServer.Core.Models;

public partial class Todo : ObservableObject {
    public uint Id { get; init; }
    [ObservableProperty] private uint? todoSectionId;
    [ObservableProperty] private uint order;
    [ObservableProperty] private string content = string.Empty;
    [ObservableProperty] private bool isDone;
    
    public uint CreatedBy { get; init; }
    public DateTime CreatedAt { get; set; }
    
    public uint? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    //public List<SqliteBlob> Files; // Maybe sam link do files??
}