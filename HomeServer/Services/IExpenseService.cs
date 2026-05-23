using HomeServer.DTOs.Expenses;
using HomeServer.Models.Expenses;

namespace HomeServer.Services;

public interface IExpenseService {
    
    Task<IReadOnlyCollection<ExpenseCategoryReadDTO>> GetAllAsync();
       
    Task<ExpenseReadDTO> GetByGuidAsync(Guid guid);

    Task<Guid> CreateAsync(ExpenseCreateDTO createDto);
    
    Task UpdateAsync(Guid guid, ExpenseUpdateDTO updateDto);

    Task DeleteAsync(Guid guid);
    
    Task<ExpenseCategoryReadDTO> GetCategoryByGuidAsync(Guid guid);

    Task<Guid> CreateCategoryAsync(ExpenseCategoryCreateDTO categoryCreateDto);

    Task UpdateCategoryAsync(Guid guid, ExpenseCategoryUpdateDTO categoryUpdateDto);

    Task DeleteCategoryAsync(Guid guid);
    
    Task<ExpenseTargetReadDTO> GetTargetByGuidAsync(Guid guid);

    Task<Guid> CreateTargetAsync(ExpenseTargetCreateDTO targetCreateDto);

    Task UpdateTargetAsync(Guid guid, ExpenseTargetUpdateDTO targetUpdateDto);

    Task DeleteTargetAsync(Guid guid);
}