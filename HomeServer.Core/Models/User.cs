using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Core.Models;

[Index(nameof(Email), IsUnique = true)]
public class User {
    public uint Id { get; init; }
    public Guid Guid { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    [EmailAddress] public required string Email { get; set; }
    
    public required string Password { get; init; }
    public required string PasswordSalt { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}