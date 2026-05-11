using System.ComponentModel.DataAnnotations;

namespace HomeServer.Core.DTOs.User;

public class RegisterDTO {
    [MaxLength(40)] public required string FirstName { get; set; }
    [MaxLength(40)] public required string LastName { get; set; }
    [MaxLength(60)] [EmailAddress] public required string Email { get; set; }
    [MinLength(12)] [MaxLength(24)] public required string Password { get; set; }
    [MinLength(12)] [MaxLength(24)] public required string PasswordConfirm { get; set; }
}