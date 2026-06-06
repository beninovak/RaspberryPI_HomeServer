using System.ComponentModel.DataAnnotations;

namespace HomeServer.DTOs.User;

public class LoginDTO {
    [MaxLength(60)] [EmailAddress] public required string Email { get; init; }
    [MinLength(12)] [MaxLength(24)] public required string Password { get; init; }
}