using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace HomeServer.Client.ViewModels;

public partial class MainWindowViewModel : ViewModelBase {

    public enum PAGES {
        TODOS,
        EXPENSES,
        FILES,
        INFO,
    }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(OpenPageCommand))]
    private PAGES currentPage = PAGES.TODOS;
    
    [RelayCommand(CanExecute = nameof(CanOpenPage))]
    // TODO - Baje nej bi uporabu async Task: https://docs.avaloniaui.net/docs/how-to/mvvm-how-to#async-command
    private void OpenPage(PAGES pageToOpen) {
        Console.WriteLine("TEST");
    }

    public void OnStart() {
        
    }

    private bool CanOpenPage() => true;
}