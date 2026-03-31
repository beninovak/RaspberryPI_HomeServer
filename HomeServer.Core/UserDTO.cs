namespace HomeServer.Core;

public class UserDTO {
    public required string Email { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    
    // Password is not required because I only need to send it, but never get it back 
    public string Password { get; init; } = string.Empty;
}