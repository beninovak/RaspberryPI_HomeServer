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
    
    public static User DTOToUser(UserDTO userDTO) {
        return new User {
            Email = userDTO.Email,
            FirstName = userDTO.FirstName,
            LastName = userDTO.LastName,
            Password = userDTO.Password,
        };
    }
}