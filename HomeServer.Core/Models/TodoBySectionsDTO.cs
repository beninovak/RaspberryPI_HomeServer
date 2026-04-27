using System.Text.Json.Serialization;

namespace HomeServer.Core.Models;

public struct TodoBySectionsDTO {
    public uint Id { get; set; }
    public uint Order { get; set; }
    public string Title { get; set; }
    public List<Todo>? Todos { get; set; } 
}