using Microsoft.EntityFrameworkCore;
using HomeServer.Core.Models;

namespace HomeServer.Server.Data;

public class MainDbContext(DbContextOptions<MainDbContext> options) : DbContext(options) {
    public DbSet<User> Users => Set<User>();
    public DbSet<Todo> Todos => Set<Todo>();
    public DbSet<TodoCategory> TodoCategories => Set<TodoCategory>();
    public DbSet<Expense> Expenses => Set<Expense>();
    public DbSet<ExpenseCategory> ExpenseCategories => Set<ExpenseCategory>();
    public DbSet<ExpenseTarget> ExpenseTargets => Set<ExpenseTarget>();
    public DbSet<ExpenseTargetHistory> ExpenseTargetHistory => Set<ExpenseTargetHistory>();
}