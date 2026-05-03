using HomeServer.Core.Models;
using HomeServer.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Server.Repositories;

public class UserRepository(MainDbContext db) : IUserRepository {

    public async Task CreateUser(User user) {
        await db.Users.AddAsync(user);
        await db.SaveChangesAsync();
    }
    
    public async Task<User?> GetUserByEmail(string email) {
        return await db.Users.FirstOrDefaultAsync(dbUser => dbUser.Email.Equals(email));
    }
}