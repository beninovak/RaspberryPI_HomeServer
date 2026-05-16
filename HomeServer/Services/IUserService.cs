using HomeServer.DTOs;
using HomeServer.DTOs.User;

namespace HomeServer.Services;

public interface IUserService {
    public Task RegisterUserAsync(RegisterDTO user);
    public Task LoginUserAsync(LoginDTO loginUser);
}