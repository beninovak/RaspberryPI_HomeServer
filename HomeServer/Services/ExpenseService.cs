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
            Expenses = ModelToReadDTOMany(category.Expenses),
            Color = category.Color,
            Target = category.Target is null ? null : TargetModelToReadDTO(category.Target),
        }).ToList();
    }
    
    public async Task<ExpenseReadDTO> GetByGuidAsync(Guid guid) {
        Expense? expense = await expenseRepository.GetByGuidAsync(guid);
        if (expense is null) {
            throw new Exception("No expense");
        }
        return ModelToReadDTO(expense);
    }
    
    public async Task<Guid> CreateAsync(ExpenseCreateDTO createDto) {
        Expense newExpense = CreateDTOToModel(createDto);
        await expenseRepository.CreateAsync(newExpense);
        return newExpense.Guid;
    }
    
    public async Task UpdateAsync(Guid guid, ExpenseUpdateDTO updateDto) {
        await expenseRepository.UpdateAsync(UpdateDTOToModel(guid, updateDto));
    }
    
    public async Task DeleteAsync(Guid guid) {
        Expense? deleteExpense = await expenseRepository.GetByGuidAsync(guid);
        if (deleteExpense is null) {
            throw new Exception("No expense");
        }
        await expenseRepository.DeleteAsync(deleteExpense);
    }
    #endregion

    #region Categories
    public async Task<ExpenseCategoryReadDTO> GetCategoryByGuidAsync(Guid guid) {
        ExpenseCategory? category = await expenseRepository.GetCategoryByGuidAsync(guid);
        if (category is null) {
            throw new Exception("No expense");
        }
        return CategoryModelToReadDTO(category);
    }
    
    public async Task<Guid> CreateCategoryAsync(ExpenseCategoryCreateDTO categoryCreateDto) {
        ExpenseCategory newCategory = CategoryCreateDTOToModel(categoryCreateDto);
        await expenseRepository.CreateCategoryAsync(newCategory);
        return newCategory.Guid;
    }
    
    public async Task UpdateCategoryAsync(Guid guid, ExpenseCategoryUpdateDTO categoryUpdateDto) {
        await expenseRepository.UpdateCategoryAsync(CategoryUpdateDTOToModel(guid, categoryUpdateDto));
    }
    
    public async Task DeleteCategoryAsync(Guid guid) {
        ExpenseCategory? deleteCategory = await expenseRepository.GetCategoryByGuidAsync(guid);
        if (deleteCategory is null) {
            throw new Exception("No category");
        }
        await expenseRepository.DeleteCategoryAsync(deleteCategory);
    }
    #endregion

    #region Targets
    public async Task<ExpenseTargetReadDTO> GetTargetByGuidAsync(Guid guid) {
        ExpenseTarget? target = await expenseRepository.GetTargetByGuidAsync(guid);
        if (target is null) {
            throw new Exception("No target");
        }
        return TargetModelToReadDTO(target);
    }
    
    public async Task<Guid> CreateTargetAsync(ExpenseTargetCreateDTO targetCreateDto) {
        ExpenseTarget newExpense = TargetCreateDTOToModel(targetCreateDto);
        await expenseRepository.CreateTargetAsync(newExpense);
        return newExpense.Guid;
    }
    
    public async Task UpdateTargetAsync(Guid guid, ExpenseTargetUpdateDTO targetUpdateDto) {
        await expenseRepository.UpdateTargetAsync(TargetUpdateDTOToModel(guid, targetUpdateDto));
    }
    
    public async Task DeleteTargetAsync(Guid guid) {
        ExpenseTarget? deleteTarget = await expenseRepository.GetTargetByGuidAsync(guid);
        if (deleteTarget is null) {
            throw new Exception("No target");
        }
        await expenseRepository.DeleteTargetAsync(deleteTarget);
    }
    #endregion
    
    #region Converters
    private ExpenseReadDTO ModelToReadDTO(Expense expense) {
        return new ExpenseReadDTO {
            Guid = expense.Guid,
            Title = expense.Title,
            Total = expense.Total,
            Description = expense.Description,
            ImagePath = expense.ImagePath,
        };
    }
    private List<ExpenseReadDTO> ModelToReadDTOMany(List<Expense> expenses) {
        List<ExpenseReadDTO> convertedList = [];
        foreach (Expense expense in expenses) {
            convertedList.Add(ModelToReadDTO(expense));
        }
        return convertedList;
    }
    private Expense CreateDTOToModel(ExpenseCreateDTO createDto) {
        return new Expense {
            Guid = Guid.NewGuid(),
            ExpenseCategoryId = createDto.ExpenseCategoryId,
            Order = createDto.Order,
            Title = createDto.Title,
            Total = createDto.Total,
            Description = createDto.Description,
            ImagePath = createDto.ImagePath,
            CreatedBy = createDto.CreatedBy,
            CreatedAt = DateTime.UtcNow,
        };
    }
    private Expense UpdateDTOToModel(Guid guid, ExpenseUpdateDTO updateDto) {
        return new Expense {
            Guid = guid,
            ExpenseCategoryId = updateDto.ExpenseCategoryId,
            Order = updateDto.Order,
            Title = updateDto.Title,
            Total = updateDto.Total,
            Description = updateDto.Description,
            ImagePath = updateDto.ImagePath,
            UpdatedBy = updateDto.UpdatedBy,
            UpdatedAt = DateTime.UtcNow,
        };
    }

    private ExpenseCategoryReadDTO CategoryModelToReadDTO(ExpenseCategory category) {
        return new ExpenseCategoryReadDTO {
            Guid = category.Guid,
            Title = category.Title,
            Color = category.Color,
            Expenses = ModelToReadDTOMany(category.Expenses),
            Target = category.Target is null ? null : TargetModelToReadDTO(category.Target),
        };
    }
    
    private ExpenseCategory CategoryCreateDTOToModel(ExpenseCategoryCreateDTO categoryCreateDto) {
        return new ExpenseCategory {
            Guid = Guid.NewGuid(),
            Order = categoryCreateDto.Order,
            Title = categoryCreateDto.Title,
            Color = categoryCreateDto.Color,
            CreatedBy = categoryCreateDto.CreatedBy,
            CreatedAt = DateTime.UtcNow,
        };
    }
    private ExpenseCategory CategoryUpdateDTOToModel(Guid guid, ExpenseCategoryUpdateDTO categoryUpdateDto) {
        return new ExpenseCategory {
            Guid = guid,
            Order = categoryUpdateDto.Order,
            Title = categoryUpdateDto.Title,
            Color = categoryUpdateDto.Color,
            UpdatedBy = categoryUpdateDto.UpdatedBy,
            UpdatedAt = DateTime.UtcNow,
        };
    }
    
    private ExpenseTargetReadDTO TargetModelToReadDTO(ExpenseTarget target) {
        return new ExpenseTargetReadDTO {
            Guid = target.Guid,
            Title = target.Title,
            Total = target.Total,
        };
    }
    private ExpenseTarget TargetCreateDTOToModel(ExpenseTargetCreateDTO targetCreateDto) {
        return new ExpenseTarget {
            Guid = Guid.NewGuid(),
            ExpenseCategoryId = targetCreateDto.ExpenseCategoryId,
            Title = targetCreateDto.Title,
            Total = targetCreateDto.Total,
            CreatedBy = targetCreateDto.CreatedBy,
            CreatedAt = DateTime.UtcNow,
        };
    }
    private ExpenseTarget TargetUpdateDTOToModel(Guid guid, ExpenseTargetUpdateDTO targetUpdateDto) {
        return new ExpenseTarget {
            Guid = guid,
            ExpenseCategoryId = targetUpdateDto.ExpenseCategoryId,
            Title = targetUpdateDto.Title,
            Total = targetUpdateDto.Total,
            UpdatedBy = targetUpdateDto.UpdatedBy,
            UpdatedAt = DateTime.UtcNow,
        };
    }
    #endregion
}