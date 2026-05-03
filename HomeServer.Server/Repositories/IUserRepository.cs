using HomeServer.Core.Models;

namespace HomeServer.Server.Repositories;

public interface IUserRepository {
    public Task CreateUser(User user);
    public Task<User?> GetUserByEmail(string email);
}