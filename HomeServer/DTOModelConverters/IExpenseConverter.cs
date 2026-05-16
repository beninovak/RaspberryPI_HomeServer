using HomeServer.DTOs.Expenses;
using HomeServer.Models.Expenses;

namespace HomeServer.DTOModelConverters;

public interface IExpenseConverter {
    public ExpenseReadDTO ExpenseModelToReadDTO(Expense expense);
    public List<ExpenseReadDTO> ExpenseModelToReadDTOMany(List<Expense> expenses);
    public Expense ExpenseCreateDTOToModel(ExpenseCreateDTO expenseCreateDto);
    public Expense ExpenseUpdateDTOToModel(Guid guid, ExpenseUpdateDTO expenseUpdateDto);
    public ExpenseCategory ExpenseCategoryCreateDTOToModel(ExpenseCategoryCreateDTO expenseCategoryCreateDto);
    public ExpenseCategory ExpenseCategoryUpdateDTOToModel(Guid guid, ExpenseCategoryUpdateDTO expenseCategoryUpdateDto);
    public ExpenseReadDTO ExpenseTargetModelToReadDTO(Expense expenseTarget);
    public ExpenseTarget ExpenseTargetCreateDTOToModel(ExpenseTargetCreateDTO expenseTargetCreateDto);
    public ExpenseTarget ExpenseTargetUpdateDTOToModel(Guid guid, ExpenseTargetUpdateDTO expenseTargetUpdateDto);
}