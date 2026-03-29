using System.ComponentModel.DataAnnotations;
using Microsoft.Data.Sqlite;

namespace RaspberryPI_HomeServer.Models;

public class Todo {
    public uint? Id { get; set; }
    //public TodoSection TodoSection { get; set; }
    public uint TodoSectionId { get; set; }
    public uint Order { get; set; }
    [MaxLength(300)]
    public string Content { get; set; } = string.Empty;
    public bool IsDone { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    //public List<SqliteBlob> Files; // Maybe??
}