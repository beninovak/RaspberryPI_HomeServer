using HomeServer.Data;
using HomeServer.DTOs.Expenses;
using HomeServer.Models.Expenses;
using HomeServer.Services;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Repositories;

public class ExpenseRepository(MainDbContext db) : IExpenseRepository {

    #region Expenses
    public async Task<IReadOnlyCollection<ExpenseCategory>> GetAllAsync(Guid userGuid) {
        return await db.ExpenseCategories
            .Include(category => category.Expenses)
            .Where(category => category.CreatedBy.Equals(userGuid))
            .Include(category => category.Target)
            .Where(category => category.Target == null || category.Target.CreatedBy.Equals(userGuid)) // TODO - prever ta Where
            .OrderBy(category => category.Order)
            .ThenBy(category => category.Title)
            .ThenBy(category => category.Expenses.Any() ? category.Expenses.First().Order : 0)
            .ToListAsync();
    }
    
    public async Task<Expense?> GetByGuidAsync(Guid guid, Guid userGuid) {
        return await db.Expenses.FirstAsync(expense => expense.Guid.Equals(guid) && expense.CreatedBy.Equals(userGuid));
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
    public async Task<ExpenseCategory?> GetCategoryByGuidAsync(Guid guid, Guid userGuid) {
        return await db.ExpenseCategories.FirstAsync(category => category.Guid.Equals(guid) && category.CreatedBy.Equals(userGuid));
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
    public async Task<ExpenseTarget?> GetTargetByGuidAsync(Guid guid, Guid userGuid) {
        return await db.ExpenseTargets.FirstAsync(target => target.Guid.Equals(guid) && target.CreatedBy.Equals(userGuid));
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