using HomeServer.DTOs.User;
using HomeServer.Models.User;

namespace HomeServer.Repositories;

public interface IUserRepository {
    public Task<string?> GetUserByGuidAsync(Guid guid);
    public Task CreateUserAsync(RegisterDTO user, string passwordSalt, string passwordHash);
    public Task<bool> UserWithEmailExistsAsync(string email);

    public Task<string> GetPasswordSaltFromUserEmailAsync(string email);
}