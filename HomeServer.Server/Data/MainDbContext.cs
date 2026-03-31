using HomeServer.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Server.Data;

public class MainDbContext : DbContext {

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) {
        
    }
    public DbSet<User> Users => Set<User>();
    public DbSet<Todo> Todos => Set<Todo>();
    public DbSet<TodoSection> TodoSections => Set<TodoSection>();
}