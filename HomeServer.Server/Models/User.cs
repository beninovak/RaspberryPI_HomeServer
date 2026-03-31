using System.ComponentModel.DataAnnotations;

namespace HomeServer.Server.Models;

public class User {
    public uint Id { get; init; }
    [MaxLength(100)] 
    public string FirstName { get; init; } = string.Empty;
    [MaxLength(100)] 
    public string LastName { get; init; } = string.Empty;
    [MaxLength(100)] 
    public string Email { get; init; } = string.Empty;
    [MaxLength(256)] 
    public string Password { get; init; } = string.Empty;
    [MaxLength(12)] 
    public string PasswordSalt { get; init; } = string.Empty;

    public DateTime CreatedOn { get; init; } = DateTime.Now;
}