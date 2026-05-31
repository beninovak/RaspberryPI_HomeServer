using HomeServer.DTOs.User;
using HomeServer.Models.User;
using HomeServer.Repositories;
using Newtonsoft.Json;

namespace HomeServer.Services;

public class LoginDataException(string message) : Exception(message);
public class RegisterDataException(string message) : Exception(message);
public class UserAlreadyExistsException(string message) : Exception(message);

public class UserService(IUserRepository userRepository) : IUserService {
    public async Task RegisterUserAsync(RegisterDTO registerDto) {
        Dictionary<string, List<string>> errors = new Dictionary<string, List<string>> {
            { "Names", [] },
            { "Email", [] },
            { "Password", [] },
        };
        
        if (string.IsNullOrEmpty(registerDto.Email)) {
            errors["Email"].Add("Email cannot be empty.");
        }
        if (string.IsNullOrEmpty(registerDto.Password)) {
            errors["Password"].Add("Password cannot be empty.");
        }
        if (string.IsNullOrEmpty(registerDto.FirstName)) {
            errors["Names"].Add("First name cannot be empty.");
        }
        if (string.IsNullOrEmpty(registerDto.LastName)) {
            errors["Names"].Add("Last name cannot be empty.");
        }
        
        char[] allowedSpecialChars = [',', '.', '!', '?', '_', '-', '(', ')', '[', ']', '@'];
        bool hasLowercase = true, hasUppercase = true, hasNumbers = true, hasSpecialChars = true;
        foreach (int asciiCode in registerDto.Password) {
            switch (asciiCode) {
                case >= 48 and <= 57:
                    hasNumbers = true;
                    break;
                case >= 65 and <= 90:
                    hasUppercase = true;
                    break;
                case >= 97 and <= 122:
                    hasLowercase = true;
                    break;
                default:
                    if (allowedSpecialChars.Contains((char)asciiCode)) {
                        hasSpecialChars = true;
                    }
                    break;
            }
        }
        
        if (!hasLowercase) {
            errors["Password"].Add("Password must contain lowercase letters.");
        }
        if (!hasUppercase) {
            errors["Password"].Add("Password must contain uppercase letters.");
        }
        if (!hasNumbers) {
            errors["Password"].Add("Password must contain numbers.");
        }
        if (!hasSpecialChars) {
            errors["Password"].Add($"Password must contain at least one allowed special character\n.{string.Join(", ", allowedSpecialChars)}");
        }
        if (registerDto.Password.Length is < 12 or > 24) {
            errors["Password"].Add("Password must be between 12 and 24 characters long.");
        }
        
        if (errors.Values.SelectMany(list => list).ToList().Any()) {
            throw new RegisterDataException(JsonConvert.SerializeObject(errors));
        }
        
        if (await userRepository.UserWithEmailExistsAsync(registerDto.Email)) {
            throw new UserAlreadyExistsException("A user with this email already exists.");
        }
        
        string passwordSalt = BCrypt.Net.BCrypt.GenerateSalt();
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password, passwordSalt, true);

        User user = new User {
            Guid = Guid.NewGuid(),
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email,
            Password = passwordHash,
            PasswordSalt = passwordSalt,
            CreatedAt = DateTime.UtcNow,
        };
        
        await userRepository.CreateUserAsync(user);
    }
    
    public async Task<Guid> LoginUserAsync(LoginDTO loginUser) {
        Dictionary<string, List<string>> errors = new Dictionary<string, List<string>> {
            { "Email", [] },
            { "Password", [] },
            { "General", [] },
        };
        
        if (string.IsNullOrEmpty(loginUser.Email)) {
            errors["Email"].Add("Email cannot be empty.");
        }

        if (string.IsNullOrEmpty(loginUser.Password)) {
            errors["Password"].Add("Password cannot be empty.");
        }

        if (!await userRepository.UserWithEmailExistsAsync(loginUser.Email)) {
            errors["Email"].Add("No user with such an email exists.");
        }

        User? user = await userRepository.GetUserByEmailAsync(loginUser.Email);
        if (user is null) {
            errors["General"].Add("No such user exists.");
        }
        
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(loginUser.Password, user?.PasswordSalt, true);
        if (!BCrypt.Net.BCrypt.EnhancedVerify(loginUser.Password, passwordHash)) {
            errors["General"].Add("Email and password do not match.");
        }
        
        if (errors.Values.SelectMany(list => list).ToList().Any()) {
            throw new LoginDataException(JsonConvert.SerializeObject(errors));
        }
        
        return user!.Guid;
    }
}