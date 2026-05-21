using HomeServer.DTOs.Expenses;
using HomeServer.Models.Expenses;
using HomeServer.Repositories;

namespace HomeServer.Services;

public class ExpenseService(IExpenseRepository expenseRepository) : IExpenseService {
    
    #region Expenses
    public async Task<List<ExpenseCategoryReadDTO>> GetAllAsync() {
        List<ExpenseCategory> expenseCategories = await expenseRepository.GetAllAsync();
        return expenseCategories.Select(category => new ExpenseCategoryReadDTO {
            Guid = category.Guid,
            Title = category.Title,
            Expenses = ExpenseModelToReadDTOMany(category.Expenses),
            Color = category.Color,
            Target = ExpenseTargetModelToReadDTO(category.Target),
        }).ToList();
    }
    
    public async Task<ExpenseReadDTO?> GetByGuidAsync(Guid guid) {
        throw new NotImplementedException();
    }
    
    public async Task<Guid> CreateAsync(ExpenseCreateDTO expenseCreateDto) {
        throw new NotImplementedException();
    }
    
    public async Task UpdateAsync(ExpenseUpdateDTO expenseUpdateDto) {
        throw new NotImplementedException();
    }
    
    public async Task DeleteAsync(Guid guid) {
        throw new NotImplementedException();
    }
    #endregion

    #region Categories
    public async Task<ExpenseCategoryReadDTO> GetCategoryByGuidAsync(Guid guid) {
        throw new NotImplementedException();
    }
    
    public async Task<Guid> CreateCategoryAsync(ExpenseCategoryCreateDTO expenseCategoryCreateDto) {
        throw new NotImplementedException();
    }
    
    public async Task UpdateCategoryAsync(Guid guid, ExpenseCategoryUpdateDTO expenseCategoryUpdateDto) {
        throw new NotImplementedException();
    }
    
    public async Task DeleteCategoryAsync(Guid guid) {
        throw new NotImplementedException();
    }
    #endregion

    #region Targets
    public async Task<ExpenseTargetReadDTO> GetTargetByGuidAsync(Guid guid) {
        throw new NotImplementedException();
    }
    
    public async Task<Guid> CreateTargetAsync(ExpenseTargetCreateDTO expenseTargetCreateDto) {
        throw new NotImplementedException();
    }
    
    public async Task UpdateTargetAsync(Guid guid, ExpenseTargetUpdateDTO expenseTargetUpdateDto) {
        throw new NotImplementedException();
    }
    
    public async Task DeleteTargetAsync(Guid guid) {
        throw new NotImplementedException();
    }
    #endregion
    
    #region Converters
    public ExpenseReadDTO ExpenseModelToReadDTO(Expense expense) {
        return new ExpenseReadDTO {
            Guid = expense.Guid,
            Title = expense.Title,
            Total = expense.Total,
            Description = expense.Description,
            ImagePath = expense.ImagePath,
        };
    }
    public List<ExpenseReadDTO> ExpenseModelToReadDTOMany(List<Expense> expenses) {
        List<ExpenseReadDTO> convertedList = [];
        foreach (Expense expense in expenses) {
            convertedList.Add(ExpenseModelToReadDTO(expense));
        }
        return convertedList;
    }
    public Expense ExpenseCreateDTOToModel(ExpenseCreateDTO expenseCreateDto) {
        return new Expense {
            Guid = Guid.NewGuid(),
            ExpenseCategoryId = expenseCreateDto.ExpenseCategoryId,
            Order = expenseCreateDto.Order,
            Title = expenseCreateDto.Title,
            Total = expenseCreateDto.Total,
            Description = expenseCreateDto.Description,
            ImagePath = expenseCreateDto.ImagePath,
            CreatedBy = expenseCreateDto.CreatedBy,
            CreatedAt = DateTime.UtcNow,
        };
    }
    public Expense ExpenseUpdateDTOToModel(Guid guid, ExpenseUpdateDTO expenseUpdateDto) {
        return new Expense {
            Guid = guid,
            ExpenseCategoryId = expenseUpdateDto.ExpenseCategoryId,
            Order = expenseUpdateDto.Order,
            Title = expenseUpdateDto.Title,
            Total = expenseUpdateDto.Total,
            Description = expenseUpdateDto.Description,
            ImagePath = expenseUpdateDto.ImagePath,
            UpdatedBy = expenseUpdateDto.UpdatedBy,
            UpdatedAt = DateTime.UtcNow,
        };
    }
    
    public ExpenseCategory ExpenseCategoryCreateDTOToModel(ExpenseCategoryCreateDTO expenseCategoryCreateDto) {
        return new ExpenseCategory {
            Guid = Guid.NewGuid(),
            Order = expenseCategoryCreateDto.Order,
            Title = expenseCategoryCreateDto.Title,
            Color = expenseCategoryCreateDto.Color,
            CreatedBy = expenseCategoryCreateDto.CreatedBy,
            CreatedAt = DateTime.UtcNow,
        };
    }
    public ExpenseCategory ExpenseCategoryUpdateDTOToModel(Guid guid, ExpenseCategoryUpdateDTO expenseCategoryUpdateDto) {
        return new ExpenseCategory {
            Guid = guid,
            Order = expenseCategoryUpdateDto.Order,
            Title = expenseCategoryUpdateDto.Title,
            Color = expenseCategoryUpdateDto.Color,
            UpdatedBy = expenseCategoryUpdateDto.UpdatedBy,
            UpdatedAt = DateTime.UtcNow,
        };
    }
    
    public ExpenseTargetReadDTO? ExpenseTargetModelToReadDTO(ExpenseTarget? expenseTarget) {
        if (expenseTarget is null) {
            return null;
        }

        return new ExpenseTargetReadDTO {
            Guid = expenseTarget.Guid,
            Title = expenseTarget.Title,
            Total = expenseTarget.Total,
        };
    }
    public ExpenseTarget ExpenseTargetCreateDTOToModel(ExpenseTargetCreateDTO expenseTargetCreateDto) {
        return new ExpenseTarget {
            Guid = Guid.NewGuid(),
            ExpenseCategoryId = expenseTargetCreateDto.ExpenseCategoryId,
            Title = expenseTargetCreateDto.Title,
            Total = expenseTargetCreateDto.Total,
            CreatedBy = expenseTargetCreateDto.CreatedBy,
            CreatedAt = DateTime.UtcNow,
        };
    }
    public ExpenseTarget ExpenseTargetUpdateDTOToModel(Guid guid, ExpenseTargetUpdateDTO expenseTargetUpdateDto) {
        return new ExpenseTarget {
            Guid = guid,
            ExpenseCategoryId = expenseTargetUpdateDto.ExpenseCategoryId,
            Title = expenseTargetUpdateDto.Title,
            Total = expenseTargetUpdateDto.Total,
            UpdatedBy = expenseTargetUpdateDto.UpdatedBy,
            UpdatedAt = DateTime.UtcNow,
        };
    }
    #endregion
}