namespace HomeServer.Core;

public class UserDTO {
    public required string Email { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;

    // Password is not required because I only need to send it, but never get it back 
    public string Password { get; init; } = string.Empty;
}