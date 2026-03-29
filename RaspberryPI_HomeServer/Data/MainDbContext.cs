using Microsoft.EntityFrameworkCore;
using RaspberryPI_HomeServer.Models;

namespace RaspberryPI_HomeServer.Data;

public class MainDbContext : DbContext {

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) {
        
    }
    
    public DbSet<Todo> Todos => Set<Todo>();
    public DbSet<TodoSection> TodoSections => Set<TodoSection>();
}