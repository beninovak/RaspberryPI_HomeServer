using HomeServer.Core.DTOs;
using HomeServer.Core.DTOs.User;

namespace HomeServer.Server.Repositories;

public interface IUserRepository {
    public Task CreateUserAsync(RegisterDTO user, string passwordSalt, string passwordHash);
    public Task<bool> UserWithEmailExistsAsync(string email);

    public Task<string> GetPasswordSaltFromUserEmailAsync(string email);
}