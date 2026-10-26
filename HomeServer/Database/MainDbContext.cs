using Microsoft.EntityFrameworkCore;
using HomeServer.Models;
using HomeServer.Models.Expenses;
using HomeServer.Models.Todos;
using HomeServer.Models.User;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace HomeServer.Data;

public class MainDbContext(DbContextOptions<MainDbContext> options) : DbContext(options) {
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) {
        configurationBuilder.Conventions.Remove<CascadeDeleteConvention>();
        configurationBuilder.Conventions.Remove<IndexAttributeConvention>();
        configurationBuilder.Conventions.Remove<ForeignKeyAttributeConvention>();
        configurationBuilder.Conventions.Remove<RelationshipDiscoveryConvention>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainDbContext).Assembly);
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Todo> Todos => Set<Todo>();
    public DbSet<TodoCategory> TodoCategories => Set<TodoCategory>();
    public DbSet<Expense> Expenses => Set<Expense>();
    public DbSet<ExpenseCategory> ExpenseCategories => Set<ExpenseCategory>();
    public DbSet<ExpenseTarget> ExpenseTargets => Set<ExpenseTarget>();
    public DbSet<ExpenseTargetHistory> ExpenseTargetsHistory => Set<ExpenseTargetHistory>();
}

public class MainDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
{
    public MainDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json")
            .Build();

        var options = new DbContextOptionsBuilder<MainDbContext>()
            .UseNpgsql(config.GetConnectionString("Default"))
            .Options;

        return new MainDbContext(options);
    }
}