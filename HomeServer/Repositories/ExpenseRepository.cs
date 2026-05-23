using HomeServer.Data;
using HomeServer.DTOs.Expenses;
using HomeServer.Models.Expenses;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Repositories;

public class ExpenseRepository(MainDbContext db) : IExpenseRepository {

    #region Expenses
    public async Task<IReadOnlyCollection<ExpenseCategory>> GetAllAsync() {
        return await db.ExpenseCategories
            .Include(category => category.Expenses)
            .Include(category => category.Target)
            .OrderBy(category => category.Order)
            .ThenBy(category => category.Title)
            .ThenBy(category => category.Expenses.Any() ? category.Expenses.First().Order : 0)
            .ToListAsync();
    }
    
    public async Task<Expense?> GetByGuidAsync(Guid guid) {
        return await db.Expenses.FindAsync(guid);
    }
    
    public async Task CreateAsync(Expense expense) {
        db.Expenses.Add(expense);
        await db.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(Expense expense) {
        db.Expenses.Update(expense);
        await db.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(Expense expense) {
        db.Expenses.Remove(expense);
        await db.SaveChangesAsync();
    }
    #endregion

    #region Categories
    public async Task<ExpenseCategory?> GetCategoryByGuidAsync(Guid guid) {
        return await db.ExpenseCategories.FindAsync(guid);
    }
    
    public async Task CreateCategoryAsync(ExpenseCategory expenseCategory) {
        db.ExpenseCategories.Add(expenseCategory);
        await db.SaveChangesAsync();
    }
    
    public async Task UpdateCategoryAsync(ExpenseCategory expenseCategory) {
        db.ExpenseCategories.Update(expenseCategory);
        await db.SaveChangesAsync();
    }
    
    public async Task DeleteCategoryAsync(ExpenseCategory expenseCategory) {
        db.ExpenseCategories.Remove(expenseCategory);
        await db.SaveChangesAsync();
    }
    #endregion
    
    #region Targets
    public async Task<ExpenseTarget?> GetTargetByGuidAsync(Guid guid) {
        return await db.ExpenseTargets.FindAsync(guid);
    }
    
    public async Task CreateTargetAsync(ExpenseTarget expenseTarget) {
        db.ExpenseTargets.Add(expenseTarget);
        await db.SaveChangesAsync();
    }
    
    public async Task UpdateTargetAsync(ExpenseTarget expenseTarget) {
        db.ExpenseTargets.Update(expenseTarget);
        await db.SaveChangesAsync();
    }

    public async Task DeleteTargetAsync(ExpenseTarget expenseTarget) {
        db.ExpenseTargets.Remove(expenseTarget);
        await db.SaveChangesAsync();
    }

    #endregion
}