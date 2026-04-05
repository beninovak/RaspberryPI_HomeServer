using System.Text;
using HomeServer.Core;
using HomeServer.Server.Models;

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
        for (int i = 0; i < length; i++) {
            type = rng.Next(0, 3);
            ch = type switch {
                0 => Convert.ToChar(rng.Next(48, 58)), // ASCII numbers
                1 => Convert.ToChar(rng.Next(65, 91)), // ASCII uppercase
                _ => Convert.ToChar(rng.Next(97, 123)), // ASCII lowercase
            };
            
            salt.Append(ch);
        }
        salt.Append((new DateTimeOffset().ToUnixTimeSeconds() * -1) / 1_000_000); // Gugu gaga
        salt.Append(rng.Next(0, 1000));
        
        return salt.ToString().Substring(rng.Next(0, salt.Length - length + 1), length);
    }
}