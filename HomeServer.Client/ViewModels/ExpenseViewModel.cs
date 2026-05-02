using Avalonia.Controls;
using HomeServer.Client.Interfaces;

namespace HomeServer.Client.ViewModels;

public class ExpenseViewModel : ViewModelBase, IDataViewModel {
    
    public string ModelName { get; init; } = "Expense"; // Temp ker še nimam Expense modela
    //public string ModelName { get; init; } = nameof(Expense);

    public string? GetDataJson() {
        return null;
    }
}