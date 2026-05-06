using System.ComponentModel.DataAnnotations;

namespace HomeServer.Core.Models;

public class LoginDTO {
    [MaxLength(60)] [EmailAddress] public required string Email { get; set; }
    [MinLength(12)] [MaxLength(24)] public required string Password { get; set; }
}