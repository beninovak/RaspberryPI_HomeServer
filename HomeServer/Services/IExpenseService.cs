using HomeServer.DTOs.Expenses;
using HomeServer.Models.Expenses;

namespace HomeServer.Services;

public interface IExpenseService {
    
    Task<List<ExpenseCategoryReadDTO>> GetAllAsync();
       
    Task<ExpenseReadDTO?> GetByGuidAsync(Guid guid);

    Task<Guid> CreateAsync(ExpenseCreateDTO expenseCreateDto);
    
    Task UpdateAsync(ExpenseUpdateDTO expenseUpdateDto);

    Task DeleteAsync(Guid guid);
    
    Task<ExpenseCategoryReadDTO> GetCategoryByGuidAsync(Guid guid);

    Task<Guid> CreateCategoryAsync(ExpenseCategoryCreateDTO expenseCategoryCreateDto);

    Task UpdateCategoryAsync(Guid guid, ExpenseCategoryUpdateDTO expenseCategoryUpdateDto);

    Task DeleteCategoryAsync(Guid guid);
    
    Task<ExpenseTargetReadDTO> GetTargetByGuidAsync(Guid guid);

    Task<Guid> CreateTargetAsync(ExpenseTargetCreateDTO expenseTargetCreateDto);

    Task UpdateTargetAsync(Guid guid, ExpenseTargetUpdateDTO expenseTargetUpdateDto);

    Task DeleteTargetAsync(Guid guid);
}