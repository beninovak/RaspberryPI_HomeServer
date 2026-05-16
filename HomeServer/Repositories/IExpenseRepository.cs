using HomeServer.DTOs.Expenses;

namespace HomeServer.Repositories;

public interface IExpenseRepository {
    Task<List<ExpenseCategoryReadDTO>> GetAllExpensesAsync();
    Task<ExpenseReadDTO?> GetExpenseByGuidAsync(Guid guid);

    Task CreateExpenseAsync(ExpenseCreateDTO expenseCreateDto);

    Task<bool> UpdateExpenseAsync(Guid guid, ExpenseUpdateDTO expenseUpdateDto);
    
    /// <param name="guid">The Guid of the item to delete</param>
    /// <returns>true if the deletion was successful, false if the item was not found.</returns>
    Task<bool> DeleteExpenseAsync(Guid guid);

    Task CreateExpenseCategoryAsync(ExpenseCategoryCreateDTO expenseCategoryCreateDto);

    Task UpdateExpenseCategoryAsync(Guid guid, ExpenseCategoryUpdateDTO expenseCategoryUpdateDto);

    /// <param name="guid">The Guid of the category to delete</param>
    /// <returns>true if the deletion was successful, false if the item was not found.</returns>
    Task<bool> DeleteExpenseCategoryAsync(Guid guid);
    
    Task CreateExpenseTargetAsync(ExpenseTargetCreateDTO expenseTargetCreateDto);

    Task UpdateExpenseTargetAsync(Guid guid, ExpenseTargetUpdateDTO expenseTargetUpdateDto);
}