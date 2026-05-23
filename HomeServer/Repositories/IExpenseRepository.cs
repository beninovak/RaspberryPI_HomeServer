using HomeServer.DTOs.Expenses;
using HomeServer.Models.Expenses;

namespace HomeServer.Repositories;

public interface IExpenseRepository {
    Task<IReadOnlyCollection<ExpenseCategory>> GetAllAsync();
    
    Task<Expense?> GetByGuidAsync(Guid guid);

    Task CreateAsync(Expense expense);

    Task UpdateAsync(Expense expense);
    
    Task DeleteAsync(Expense expense);

    Task<ExpenseCategory?> GetCategoryByGuidAsync(Guid guid);
    
    Task CreateCategoryAsync(ExpenseCategory expenseCategory);

    Task UpdateCategoryAsync(ExpenseCategory expenseCategory);

    Task DeleteCategoryAsync(ExpenseCategory expenseCategory);
    
    Task<ExpenseTarget?> GetTargetByGuidAsync(Guid guid);
    
    Task CreateTargetAsync(ExpenseTarget expenseTarget);

    Task UpdateTargetAsync(ExpenseTarget expenseTarget);
    
    Task DeleteTargetAsync(ExpenseTarget expenseTarget);
}