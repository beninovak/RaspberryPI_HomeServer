namespace HomeServer.Core;

public struct TodoDTO {
    public uint Id { get; set; }
    public uint Order { get; set; }
    public string Content { get; set; }
    public bool IsDone { get; set; }
}