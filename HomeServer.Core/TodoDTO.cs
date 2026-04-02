namespace HomeServer.Core;

public class TodoDTO {
    public uint Id { get; init; }
    public uint Order { get; set; }
    public string Content { get; init; } = string.Empty; // TODO - limit to 300 chars, like in model
    public bool IsDone { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.Now;
}