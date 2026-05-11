using HomeServer.Core.DTOs;
using HomeServer.Core.DTOs.User;

namespace HomeServer.Server.Services;

public interface IUserService {
    public Task RegisterUserAsync(RegisterDTO user);
    public Task LoginUserAsync(LoginDTO loginUser);
}