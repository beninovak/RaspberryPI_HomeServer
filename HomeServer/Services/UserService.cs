using HomeServer.DTOs;
using HomeServer.DTOs.User;
using HomeServer.Models;
using HomeServer.Exceptions;
using HomeServer.Repositories;

namespace HomeServer.Services;

public class UserService(IUserRepository userRepository) : IUserService {
    public async Task RegisterUserAsync(RegisterDTO registerDto) {
        ValidateRegister(registerDto);
        
        if (await userRepository.UserWithEmailExistsAsync(registerDto.Email)) {
            throw new NonExistentUserException("A user with this email already exists.");
        }
        
        string passwordSalt = BCrypt.Net.BCrypt.GenerateSalt();
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password, passwordSalt, true);

        await userRepository.CreateUserAsync(registerDto, passwordSalt, passwordHash);
    }
    
    public async Task LoginUserAsync(LoginDTO loginUser) {
        ValidateLogin(loginUser.Email, loginUser.Password);

        if (await userRepository.UserWithEmailExistsAsync(loginUser.Email)) {
            throw new NonExistentUserException("No user with such an email exists.");
        }

        string dbSalt = await userRepository.GetPasswordSaltFromUserEmailAsync(loginUser.Email);
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(loginUser.Password, dbSalt, true);
        
        if (!BCrypt.Net.BCrypt.EnhancedVerify(loginUser.Password, passwordHash)) {
            throw new UserInvalidLoginException("Email and password do not match.");
        }
    }

    private static void ValidateRegister(RegisterDTO registerDto) {
        ValidateEmailAndPassword(registerDto.Email, registerDto.Password);

        List<Exception> errors = [];
        if (string.IsNullOrEmpty(registerDto.FirstName)) {
            errors.Add(new FirstNameValidationException("First name cannot be empty."));
        }
        if (string.IsNullOrEmpty(registerDto.LastName)) {
            errors.Add(new LastNameValidationException("Last name cannot be empty."));
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
            errors.Add(new PasswordValidationException("Password must contain lowercase letters."));
        }
        if (!hasUppercase) {
            errors.Add(new PasswordValidationException("Password must contain uppercase letters."));
        }
        if (!hasNumbers) {
            errors.Add(new PasswordValidationException("Password must contain numbers."));
        }
        if (!hasSpecialChars) {
            errors.Add(new PasswordValidationException
                ($"Password must contain at least one allowed special character\n.{string.Join(", ", allowedSpecialChars)}")
            );
        }
        if (registerDto.Password.Length is < 12 or > 24) {
            errors.Add(new PasswordValidationException("Password must be between 12 and 24 characters long."));
        }
        
        if (errors.Any()) {
            throw new AggregateException(errors);
        }
    }
    
    private static void ValidateLogin(string email, string password) {
        ValidateEmailAndPassword(email, password);
    }

    private static void ValidateEmailAndPassword(string email, string password) {
        List<Exception> errors = [];
        
        if (string.IsNullOrEmpty(email)) {
            errors.Add(new EmailValidationException("Email cannot be empty."));
        }
        // TODO - Dodej nek email regex
        // emailErrors.Add("Invalid email format");

        if (string.IsNullOrEmpty(password)) {
            errors.Add(new PasswordValidationException("Password cannot be empty."));
        }

        if (errors.Any()) {
            throw new AggregateException(errors);
        }
    }
}