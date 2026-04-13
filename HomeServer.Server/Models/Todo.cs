using System.ComponentModel.DataAnnotations;
using Microsoft.Data.Sqlite;

namespace HomeServer.Server.Models;

public class Todo {
    public uint Id { get; init; }
    public uint TodoSectionId { get; init; }
    public uint Order { get; init; }
    [MaxLength(300)] public string Content { get; init; } = string.Empty;
    public bool IsDone { get; init; } = false;
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    public required uint CreatedBy { get; init; }

    //public List<SqliteBlob> Files; // Maybe sam link do files??
}