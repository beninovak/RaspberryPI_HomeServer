using System.Text;
using HomeServer.Core;
using HomeServer.Server.Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HomeServer.Server.Utils;

public static class UserUtil {
    public static UserDTO UserToDTO(User user) {
        return new UserDTO {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
        };
    }

    // public static User DTOToUser(UserDTO userDTO) {
    //     return new User {
    //         Email = userDTO.Email,
    //         FirstName = userDTO.FirstName,
    //         LastName = userDTO.LastName,
    //         Password = userDTO.Password,
    //     };
    // }

    public static string GeneratePasswordSalt(int length) {
        Random rng = new Random();
        StringBuilder salt = new StringBuilder();
        salt.Append(rng.Next(0, 1000));

        int type;
        char ch;
        for (int i = 0; i < length * 10; i++) { // *10 to make the salt long
            type = rng.Next(0, 3);
            ch = type switch {
                0 => Convert.ToChar(rng.Next(48, 58)), // ASCII numbers
                1 => Convert.ToChar(rng.Next(65, 91)), // ASCII uppercase
                _ => Convert.ToChar(rng.Next(97, 123)), // ASCII lowercase
            };

            salt.Append(ch);
        }

        // Gugu gaga...neka cifra. Pomnožiš z -1, ker ti ToUnixTimeSeconds() da negativno število???
        salt.Append((new DateTimeOffset().ToUnixTimeSeconds() * -1) / 1_000_000);

        return salt.ToString().Substring(rng.Next(0, salt.Length - length + 1), length);
    }

    public static string GenerateHashedPassword(string password) {
        // TODO - Baje bi mogu uporablat PasswordHasher??
        // https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-10.0
        SHA256 sha256 = SHA256.Create();
        UTF8Encoding utf8 = new UTF8Encoding();
        string passwordSalt = GeneratePasswordSalt(Constants.PASSWORD_SALT_LENGTH);
        string hashedPassword = utf8.GetString(sha256.ComputeHash(utf8.GetBytes(password + passwordSalt)));
        // TODO - Validate hashedPassword? How tho?
        // TODO - Maybe make sure userDTO.Password and dbUser.PasswordSalt are not null?
        return hashedPassword;
    }

    public static (bool, string) IsEmailValid(string email) {
        // TODO - real logic
        bool isValid = true;
        string message = string.Empty;
        if (string.IsNullOrEmpty(email)) {
            isValid = false;
            message = "No email provided in request!";
        }
        
        // TODO - Add simple regex here
        //isValid = false;
        //message = "Email is not valid!";

        return (isValid, message);
    }

    public static (bool, string) IsPasswordValid(string password) {
        // TODO - real logic
        bool isValid = true;
        string message = string.Empty;
        if (string.IsNullOrEmpty(password)) {
            isValid = false;
            message = "No password provided in request!";
        }

        // TODO - Add password regexs
        //isValid = false;
        //message = "Email is not valid!";
        
        return (isValid, message);
    }
}