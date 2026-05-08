using HomeServer.Core.DTOs;

namespace HomeServer.Server.Repositories;

public interface IUserRepository {
    public Task CreateUser(RegisterDTO user, string passwordSalt, string passwordHash);
    public Task<bool> UserWithEmailExists(string email);

    public Task<string> GetPasswordSaltFromUserEmail(string email);
}