using System.ComponentModel.DataAnnotations;

namespace HomeServer.DTOs.User;

public class RegisterDTO {
    [MaxLength(40)] public required string FirstName { get; init; }
    [MaxLength(40)] public required string LastName { get; init; }
    [MaxLength(60)] /*[EmailAddress]*/ public required string Email { get; init; }
    [MinLength(12)] [MaxLength(24)] public required string Password { get; init; }
    [MinLength(12)] [MaxLength(24)] public required string PasswordConfirm { get; init; }
}