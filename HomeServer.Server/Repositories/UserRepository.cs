using HomeServer.Core.DTOs;
using HomeServer.Core.DTOs.User;
using HomeServer.Core.Models;
using HomeServer.Core.Models.User;
using HomeServer.Server.Data;
using HomeServer.Server.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Server.Repositories;

public class UserRepository(MainDbContext db) : IUserRepository {
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