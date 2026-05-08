using HomeServer.Core.DTOs;

namespace HomeServer.Server.Services;

public interface IUserService {
    public Task RegisterUser(RegisterDTO user);
    public Task LoginUser(LoginDTO loginUser);
}