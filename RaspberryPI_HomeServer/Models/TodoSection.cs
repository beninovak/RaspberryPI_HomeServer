using System.ComponentModel.DataAnnotations;

namespace RaspberryPI_HomeServer.Models;

public class TodoSection {
    public uint Id { get; init; }
    public uint Order { get; set; }
    
    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}