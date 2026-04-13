using System.ComponentModel.DataAnnotations;

namespace HomeServer.Core;

public class TodoDTO {
    public uint TodoSectionId { get; init; }
    public uint Order { get; set; }

    [MaxLength(300)] public string Content { get; init; } = string.Empty;
    public bool IsDone { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.Now;

    public uint CreatedBy { get; init; }
}