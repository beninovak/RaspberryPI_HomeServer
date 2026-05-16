using HomeServer.DTOs.User;
using HomeServer.Models.User;
using HomeServer.Data;
using HomeServer.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Repositories;

public class UserRepository(MainDbContext db) : IUserRepository {
    public async Task<string?> GetUserByGuidAsync(Guid guid) {
        User? dbUser = await db.Users.FindAsync(guid);
        return dbUser?.Email;
    }
    public async Task CreateUserAsync(RegisterDTO registerDto, string passwordSalt, string passwordHash) {
        await db.Users.AddAsync(new User {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email,
            Password = passwordHash,
            PasswordSalt = passwordSalt,
            CreatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
        });
        await db.SaveChangesAsync();
    }
    
    public async Task<bool> UserWithEmailExistsAsync(string email) {
        return await db.Users.AnyAsync(dbUser => dbUser.Email.Equals(email));
    }
    
    public async Task<string> GetPasswordSaltFromUserEmailAsync(string email) {
        User? dbUser = await db.Users.FirstOrDefaultAsync(dbUser => dbUser.Email.Equals(email));
        if (dbUser is null) {
            throw new NonExistentUserException("Cannot get password salt, because no user with this email exists!");
        }
        return dbUser.PasswordSalt;
    }
}