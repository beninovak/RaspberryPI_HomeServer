using Microsoft.EntityFrameworkCore;
using HomeServer.Core.Models;
using HomeServer.Core.Models.Expenses;
using HomeServer.Core.Models.Todos;
using HomeServer.Core.Models.User;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace HomeServer.Server.Data;

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