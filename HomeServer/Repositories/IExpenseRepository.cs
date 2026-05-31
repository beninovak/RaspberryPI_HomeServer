using HomeServer.DTOs.Expenses;
using HomeServer.Models.Expenses;

namespace HomeServer.Repositories;

public interface IExpenseRepository {
    Task<IReadOnlyCollection<ExpenseCategory>> GetAllAsync(Guid userGuid);
    
    Task<Expense?> GetByGuidAsync(Guid guid, Guid userGuid);

    Task CreateAsync(Expense expense);

    Task UpdateAsync(Expense expense);
    
    Task DeleteAsync(Expense expense);

    Task<ExpenseCategory?> GetCategoryByGuidAsync(Guid guid, Guid userGuid);
    
    Task CreateCategoryAsync(ExpenseCategory expenseCategory);

    Task UpdateCategoryAsync(ExpenseCategory expenseCategory);

    Task DeleteCategoryAsync(ExpenseCategory expenseCategory);
    
    Task<ExpenseTarget?> GetTargetByGuidAsync(Guid guid, Guid userGuid);
    
    Task CreateTargetAsync(ExpenseTarget expenseTarget);

    Task UpdateTargetAsync(ExpenseTarget expenseTarget);
    
    Task DeleteTargetAsync(ExpenseTarget expenseTarget);
}