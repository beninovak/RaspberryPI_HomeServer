using Shared.DTOs.User;

namespace HomeServer.Services;

public interface IUserService {
    public Task<RegisterResult> RegisterUserAsync(RegisterDTO user);
    public Task<LoginResult> LoginUserAsync(LoginDTO loginUser);
}