using HomeServer.Core.Models;
using HomeServer.Server.Exceptions;
using HomeServer.Server.Repositories;

namespace HomeServer.Server.Services;

public class UserService(IUserRepository userRepository) : IUserService {
    public async Task RegisterUser(RegisterDTO registerDto) {
        ValidateRegister(registerDto);
        
        string passwordSalt = BCrypt.Net.BCrypt.GenerateSalt();
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password, passwordSalt, true);

        await userRepository.CreateUser(new User {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email,
            Password = passwordHash,
            PasswordSalt = passwordSalt,
            CreatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
        });
    }
    
    public async Task LoginUser(LoginDTO loginUser) {
        ValidateLogin(loginUser.Email, loginUser.Password);
        
        User? dbUser = await userRepository.GetUserByEmail(loginUser.Email);
        if (dbUser is null) {
            throw new NonExistentUserException("No user with such an email exists.");
        }
        
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(loginUser.Password, dbUser.PasswordSalt, true);
        if (!BCrypt.Net.BCrypt.EnhancedVerify(dbUser.Password, passwordHash)) {
            throw new UserInvalidLoginException("Email and password do not match.");
        }
    }

    private static void ValidateRegister(RegisterDTO registerDto) {
        var errors = ValidateEmailAndPassword(registerDto.Email, registerDto.Password);

        if (string.IsNullOrEmpty(registerDto.FirstName)) {
            errors.Add(new FirstNameValidationException("First name cannot be empty."));
        }
        if (string.IsNullOrEmpty(registerDto.LastName)) {
            errors.Add(new LastNameValidationException("Last name cannot be empty."));
        }
        
        if (!registerDto.Password.Equals(registerDto.PasswordConfirm)) {
            errors.Add(new PasswordValidationException("Passwords do not match."));
        }
        
        if (errors.Any()) {
            throw new AggregateException(errors);
        }
    }
    
    private static void ValidateLogin(string email, string password) {
        var errors = ValidateEmailAndPassword(email, password);
        if (errors.Any()) {
            throw new AggregateException(errors);
        }
    }

    private static List<Exception> ValidateEmailAndPassword(string email, string password) {
        List<Exception> errors = [];
        
        if (string.IsNullOrEmpty(email)) {
            errors.Add(new EmailValidationException("Email cannot be empty."));
        }
        // TODO - Dodej nek email regex
        // emailErrors.Add("Invalid email format");

        if (string.IsNullOrEmpty(password)) {
            errors.Add(new PasswordValidationException("Password cannot be empty."));
        }
        // TODO - Dodej nek password regex za min/max length, numbers, capital letters, special chars etc.
        // errors.Add(new PasswordValidationException("Password must be between 12 and 24 characters long."));
        // errors.Add(new PasswordValidationException("Password must contain lowercase letters."));
        // errors.Add(new PasswordValidationException("Password must contain uppercase letters."));
        // errors.Add(new PasswordValidationException("Password must contain numbers."));
        // errors.Add(new PasswordValidationException("Password must contain special characters."));
        
        return errors;
    }
}