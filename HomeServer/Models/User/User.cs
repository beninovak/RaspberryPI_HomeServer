using System.ComponentModel.DataAnnotations;

namespace HomeServer.Models.User;

public class User {
    public Guid Guid { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    [EmailAddress] public required string Email { get; init; }
    public required string Password { get; init; }
    public required string PasswordSalt { get; init; }
    public required DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}