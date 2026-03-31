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
}