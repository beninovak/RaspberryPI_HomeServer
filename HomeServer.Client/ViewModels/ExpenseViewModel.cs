using Avalonia.Controls;
using HomeServer.Client.Interfaces;

namespace HomeServer.Client.ViewModels;

public class ExpenseViewModel : ViewModelBase, IDataViewModel {
    public string SaveFileName { get; init; } = "expenses_save.json";
    public string ChangesFileName { get; init; } = "expenses_changes.json";

    public string? GetDataJson() {
        return null;
    }
}