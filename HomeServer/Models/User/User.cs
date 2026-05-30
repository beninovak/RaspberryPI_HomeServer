using System.ComponentModel.DataAnnotations;

namespace HomeServer.Models.User;

public class User {
    public required Guid Guid { get; init; }
    [MaxLength(40)] public required string FirstName { get; init; }
    [MaxLength(40)] public required string LastName { get; init; }
    [MaxLength(60)] [EmailAddress] public required string Email { get; init; }
    [MaxLength(64)] public required string Password { get; init; }
    [MaxLength(32)] public required string PasswordSalt { get; init; }
    public required DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}