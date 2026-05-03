using System.ComponentModel.DataAnnotations;

namespace HomeServer.Core.Models;

public class RegisterDTO {
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    [MaxLength(40)] [EmailAddress] public required string Email { get; set; }
    [MinLength(12)] [MaxLength(24)] public required string Password { get; set; }
    [MinLength(12)] [MaxLength(24)] public required string PasswordConfirm { get; set; }
}