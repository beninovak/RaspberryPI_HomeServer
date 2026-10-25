using HomeServer.DTOs.User;
using HomeServer.Models.User;
using HomeServer.Repositories;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;

namespace HomeServer.Services;

public class LoginDataException(string message) : Exception(message);
public class RegisterDataException(string message) : Exception(message);
public class UserAlreadyExistsException(string message) : Exception(message);

public class UserService(IUserRepository userRepository) : IUserService {
    private const string names = "Names";
    private const string email = "Email";
    private const string password = "Password";
    private const string general = "General";
    
    public async Task RegisterUserAsync(RegisterDTO registerDto) {
        Dictionary<string, List<string>> errors = new Dictionary<string, List<string>> {
            { names, [] },
            { email, [] },
            { password, [] },
        };
        
        if (string.IsNullOrEmpty(registerDto.Email)) {
            errors[email].Add("Email cannot be empty.");
        }
        if (string.IsNullOrEmpty(registerDto.Password)) {
            errors[password].Add("Password cannot be empty.");
        }
        if (string.IsNullOrEmpty(registerDto.FirstName)) {
            errors[names].Add("First name cannot be empty.");
        }
        if (string.IsNullOrEmpty(registerDto.LastName)) {
            errors[names].Add("Last name cannot be empty.");
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
            errors[password].Add("Password must contain lowercase letters.");
        }
        if (!hasUppercase) {
            errors[password].Add("Password must contain uppercase letters.");
        }
        if (!hasNumbers) {
            errors[password].Add("Password must contain numbers.");
        }
        if (!hasSpecialChars) {
            errors[password].Add($"Password must contain at least one allowed special character\n.{string.Join(", ", allowedSpecialChars)}");
        }
        if (registerDto.Password.Length is < 12 or > 24) {
            errors[password].Add("Password must be between 12 and 24 characters long.");
        }
        
        if (errors.Values.SelectMany(list => list).ToList().Any()) {
            throw new RegisterDataException(JsonConvert.SerializeObject(errors));
        }
        
        if (await userRepository.UserWithEmailExistsAsync(registerDto.Email)) {
            throw new UserAlreadyExistsException(JsonConvert.SerializeObject(new {
                General = "A user with this email already exists."
            }));
        }
        
        string passwordSalt = BCrypt.Net.BCrypt.GenerateSalt();
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password, passwordSalt);

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
            { email, [] },
            { password, [] },
            { general, [] },
        };
        
        if (string.IsNullOrEmpty(loginUser.Email)) {
            errors[email].Add("Email cannot be empty.");
        }

        if (string.IsNullOrEmpty(loginUser.Password)) {
            errors[password].Add("Password cannot be empty.");
        }

        if (!await userRepository.UserWithEmailExistsAsync(loginUser.Email)) {
            errors[email].Add("No user with such an email exists.");
        }

        User? user = await userRepository.GetUserByEmailAsync(loginUser.Email);
        if (user is null) {
            errors[general].Add("No such user exists.");
        }
        
        //string passwordHash = BCrypt.Net.BCrypt.HashPassword(loginUser.Password, user?.PasswordSalt, true);
        if (!BCrypt.Net.BCrypt.Verify(loginUser.Password, user?.Password)) {
            errors[general].Add("Email and password do not match.");
        }

        if (errors.Any()) { // TODO - continue here...zakva ta serialize doda key "errors"?
            throw new LoginDataException(JsonConvert.SerializeObject(errors));
        }
        
        return user!.Guid;
    }
}