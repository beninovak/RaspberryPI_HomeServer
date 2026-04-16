namespace HomeServer.Core;

public struct TodoBySectionsDTO {
    public uint Id { get; set; }
    public uint Order { get; set; }
    public string Title { get; set; }
    public List<TodoDTO> Todos { get; set; } 
}