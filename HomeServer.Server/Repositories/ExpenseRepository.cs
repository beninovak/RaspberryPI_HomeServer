using HomeServer.Core.DTOModelConverters;
using HomeServer.Core.DTOs.Expenses;
using HomeServer.Core.Models.Expenses;
using HomeServer.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Server.Repositories;

public class ExpenseRepository(MainDbContext db, IExpenseConverter expenseConverter) : IExpenseRepository {

    #region Expenses
    public async Task<List<ExpenseCategoryReadDTO>> GetAllExpensesAsync() {
        List<ExpenseCategory> expenseCategories = await db.ExpenseCategories
            .Include(category => category.Expenses)
            .Include(category => category.Target)
            .OrderBy(category => category.Order)
            .ThenBy(category => category.Title)
            .ThenBy(category => category.Expenses.Any() ? category.Expenses.First().Order : 0)
            .ToListAsync();

        List<ExpenseCategoryReadDTO> ExpenseCategoryReadDTOs = [];
        ExpenseCategoryReadDTOs.AddRange(
            expenseCategories.Select(category => new ExpenseCategoryReadDTO {
                Guid = category.Guid,
                Title = category.Title,
                Expenses = expenseConverter.ExpenseModelToReadDTOMany(category.Expenses),
                Color = category.Color,
                Target = expenseConverter.ExpenseTargetModelToReadDTO(category.Target),
            }));

        return ExpenseCategoryReadDTOs;
    }
    public async Task<ExpenseReadDTO?> GetExpenseByGuidAsync(Guid guid) {
        Expense? expense = await db.Expenses.FirstOrDefaultAsync(expense => expense.Guid.Equals(guid));
        return expense is null ? null : expenseConverter.ExpenseModelToReadDTO(expense);
    }
    public async Task CreateExpenseAsync(ExpenseCreateDTO expenseCreateDto) {
        Expense expense = expenseConverter.ExpenseCreateDTOToModel(expenseCreateDto);
        db.Expenses.Add(expense);
        await db.SaveChangesAsync();
    }
    public async Task<bool> UpdateExpenseAsync(Guid guid, ExpenseUpdateDTO expenseUpdateDto) {
        if (await GetExpenseByGuidAsync(guid) is null) {
            return false;
        }
        
        Expense expense = expenseConverter.ExpenseUpdateDTOToModel(guid, expenseUpdateDto);
        db.Expenses.Update(expense);
        await db.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteExpenseAsync(Guid guid) {
        Expense? expense = await db.Expenses.FirstOrDefaultAsync(expense => expense.Guid.Equals(guid));
        if (expense is null) {
            return false;
        }
        db.Expenses.Remove(expense);
        await db.SaveChangesAsync();
        return true;
    }
    #endregion

    #region Category
    public async Task CreateExpenseCategoryAsync(ExpenseCategoryCreateDTO expenseCategoryCreateDto) {
        ExpenseCategory expenseCategory = expenseConverter.ExpenseCategoryCreateDTOToModel(expenseCategoryCreateDto);
        db.ExpenseCategories.Add(expenseCategory);
        await db.SaveChangesAsync();
    }
    public async Task UpdateExpenseCategoryAsync(Guid guid, ExpenseCategoryUpdateDTO expenseCategoryUpdateDto) {
        ExpenseCategory expenseCategory = expenseConverter.ExpenseCategoryUpdateDTOToModel(guid, expenseCategoryUpdateDto);
        db.ExpenseCategories.Update(expenseCategory);
        await db.SaveChangesAsync();
    }
    public async Task<bool> DeleteExpenseCategoryAsync(Guid guid) {
        ExpenseCategory? category = await db.ExpenseCategories.FirstOrDefaultAsync(category => category.Guid.Equals(guid));
        if (category is null) {
            return false;
        }
        db.ExpenseCategories.Remove(category);
        await db.SaveChangesAsync();
        return true;
    }
    #endregion
    
    public async Task CreateExpenseTargetAsync(ExpenseTargetCreateDTO expenseTargetCreateDto) {
        ExpenseTarget expenseTarget = expenseConverter.ExpenseTargetCreateDTOToModel(expenseTargetCreateDto);
        db.ExpenseTargets.Add(expenseTarget);
        await db.SaveChangesAsync();
    }
    public async Task UpdateExpenseTargetAsync(Guid guid, ExpenseTargetUpdateDTO expenseTargetUpdateDto) {
        ExpenseTarget expenseTarget = expenseConverter.ExpenseTargetUpdateDTOToModel(guid, expenseTargetUpdateDto);
        db.ExpenseTargets.Update(expenseTarget);
        await db.SaveChangesAsync();
    }
}