using HomeServer.Core.DTOs.Expenses;

namespace HomeServer.Server.Repositories;

public class ExpenseRepository : IExpenseRepository {
    public async Task<List<ExpenseCategoryReadDTO>> GetAllExpensesAsync() {
        throw new NotImplementedException();
    }
    public async Task<ExpenseReadDTO?> GetExpenseByGuidAsync(Guid guid) {
        throw new NotImplementedException();
    }
    public async Task CreateExpenseAsync(ExpenseCreateDTO expenseCreateDto) {
        throw new NotImplementedException();
    }
    public async Task<bool> UpdateExpenseAsync(Guid guid, ExpenseUpdateDTO ExpenseUpdateDto) {
        throw new NotImplementedException();
    }
    public async Task<bool> DeleteExpenseAsync(Guid guid) {
        throw new NotImplementedException();
    }
    public async Task CreateExpenseCategoryAsync(ExpenseCategoryCreateDTO expenseCategoryCreateDto) {
        throw new NotImplementedException();
    }
    public async Task UpdateExpenseCategoryAsync(Guid guid, ExpenseCategoryUpdateDTO expenseCategoryUpdateDto) {
        throw new NotImplementedException();
    }
    public async Task<bool> DeleteExpenseCategoryAsync(Guid guid) {
        throw new NotImplementedException();
    }
    public async Task CreateExpenseTargetAsync(ExpenseTargetCreateDTO expenseTargetCreateDto) {
        throw new NotImplementedException();
    }
    public async Task<bool> UpdateExpenseTargetAsync(Guid guid, ExpenseTargetUpdateDTO ExpenseTargetUpdateDto) {
        throw new NotImplementedException();
    }
}