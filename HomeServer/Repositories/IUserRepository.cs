using HomeServer.DTOs.User;
using HomeServer.Models.User;

namespace HomeServer.Repositories;

public interface IUserRepository {
    public Task<User?> GetUserByGuidAsync(Guid guid);
    public Task<User?> GetUserByEmailAsync(string email);
    public Task CreateUserAsync(User user);
    public Task<bool> UserWithEmailExistsAsync(string email);
}