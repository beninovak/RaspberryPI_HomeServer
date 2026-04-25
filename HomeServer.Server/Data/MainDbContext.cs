using Microsoft.EntityFrameworkCore;
using HomeServer.Core.Models;

namespace HomeServer.Server.Data;

public class MainDbContext(DbContextOptions<MainDbContext> options) : DbContext(options) {
    public DbSet<User> Users => Set<User>();
    public DbSet<Todo> Todos => Set<Todo>();
    public DbSet<TodoSection> TodoSections => Set<TodoSection>();
}