using HomeServer.DTOs.User;
using HomeServer.Models.User;
using HomeServer.Data;
using HomeServer.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Repositories;

public class UserRepository(MainDbContext db) : IUserRepository {
    public async Task<User?> GetUserByGuidAsync(Guid guid) {
        return await db.Users.FindAsync(guid);
    }
    
    public async Task<User?> GetUserByEmailAsync(string email) {
        return await db.Users.FirstOrDefaultAsync(user => user.Email.Equals(email));
    }
    
    public async Task CreateUserAsync(User user) {
        await db.Users.AddAsync(user);
        await db.SaveChangesAsync();
    }
    
    public async Task<bool> UserWithEmailExistsAsync(string email) {
        return await db.Users.AnyAsync(dbUser => dbUser.Email.Equals(email));
    }
}