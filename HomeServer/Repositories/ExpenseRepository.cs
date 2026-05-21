using HomeServer.Data;
using HomeServer.DTOs.Expenses;
using HomeServer.Models.Expenses;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Repositories;

public class ExpenseRepository(MainDbContext db) : IExpenseRepository {

    #region Expenses
    public async Task<List<ExpenseCategory>> GetAllAsync() {
        return await db.ExpenseCategories
            .Include(category => category.Expenses)
            .Include(category => category.Target)
            .OrderBy(category => category.Order)
            .ThenBy(category => category.Title)
            .ThenBy(category => category.Expenses.Any() ? category.Expenses.First().Order : 0)
            .ToListAsync();
    }
    
    public async Task<Expense?> GetByGuidAsync(Guid guid) {
        Expense? expense = await db.Expenses.FindAsync(guid);
        return expense is null ? null : expenseConverter.ExpenseModelToReadDTO(expense);
    }
    
    public async Task CreateAsync(Expense expense) {
        Expense expense = expenseConverter.ExpenseCreateDTOToModel(expenseCreateDto);
        db.Expenses.Add(expense);
        await db.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(Expense expense) {
        if (await GetByGuidAsync(guid) is null) {
            return false;
        }
        
        Expense expense = expenseConverter.ExpenseUpdateDTOToModel(guid, expenseUpdateDto);
        db.Expenses.Update(expense);
        await db.SaveChangesAsync();
        return true;
    }
    
    public async Task DeleteAsync(Expense expense) {
        Expense? expense = await db.Expenses.FirstOrDefaultAsync(expense => expense.Guid.Equals(guid));
        if (expense is null) {
            return false;
        }
        db.Expenses.Remove(expense);
        await db.SaveChangesAsync();
        return true;
    }
    #endregion

    #region Categories
    public async Task<ExpenseCategory?> GetCategoryByGuidAsync(Guid guid) {
        throw new NotImplementedException();
    }
    
    public async Task CreateCategoryAsync(ExpenseCategory expenseCategory) {
        ExpenseCategory expenseCategory = expenseConverter.ExpenseCategoryCreateDTOToModel(expenseCategoryCreateDto);
        db.ExpenseCategories.Add(expenseCategory);
        await db.SaveChangesAsync();
    }
    
    public async Task UpdateCategoryAsync(ExpenseCategory expenseCategory) {
        ExpenseCategory? category = await db.ExpenseCategories.FindAsync(guid);
        if (category is null) {
            return false;
        }
        
        ExpenseCategory expenseCategory = expenseConverter.ExpenseCategoryUpdateDTOToModel(guid, expenseCategoryUpdateDto);
        db.ExpenseCategories.Update(expenseCategory);
        await db.SaveChangesAsync();
        return true;
    }
    
    public async Task DeleteCategoryAsync(ExpenseCategory expenseCategory) {
        ExpenseCategory? category = await db.ExpenseCategories.FirstOrDefaultAsync(category => category.Guid.Equals(guid));
        if (category is null) {
            return false;
        }
        db.ExpenseCategories.Remove(category);
        await db.SaveChangesAsync();
        return true;
    }
    #endregion
    
    
    #region Targets
    public async Task<ExpenseTarget?> GetTargetByGuidAsync(Guid guid) {
        throw new NotImplementedException();
    }
    
    public async Task CreateTargetAsync(ExpenseTarget expenseTarget) {
        ExpenseTarget expenseTarget = expenseConverter.ExpenseTargetCreateDTOToModel(expenseTargetCreateDto);
        db.ExpenseTargets.Add(expenseTarget);
        await db.SaveChangesAsync();
    }
    
    public async Task UpdateTargetAsync(ExpenseTarget expenseTarget) {
        ExpenseTarget? target = await db.ExpenseTargets.FindAsync(guid);
        if (target is null) {
            return false;
        }
        
        ExpenseTarget expenseTarget = expenseConverter.ExpenseTargetUpdateDTOToModel(guid, expenseTargetUpdateDto);
        db.ExpenseTargets.Update(expenseTarget);
        await db.SaveChangesAsync();
        return true;
    }

    public async Task DeleteTargetAsync(ExpenseTarget expenseTarget) {
        ExpenseTarget? target = await db.ExpenseTargets.FirstOrDefaultAsync(target => target.Guid.Equals(guid));
        if (target is null) {
            return false;
        }
        db.ExpenseTargets.Remove(target);
        await db.SaveChangesAsync();
        return true;
    }

    #endregion
}