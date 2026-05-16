using HomeServer.DTOs.Expenses;
using HomeServer.Models.Expenses;

namespace HomeServer.DTOModelConverters;

public class ExpenseConverter : IExpenseConverter {
    
    #region Expense
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
    #endregion

    #region Category
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
    #endregion

    #region Target
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