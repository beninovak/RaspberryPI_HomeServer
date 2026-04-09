using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace HomeServer.Client.ViewModels;

public partial class MainWindowViewModel : ViewModelBase {
    
    public enum PAGE {
        NONE,
        TODOS,
        EXPENSES,
        FILES,
        INFO,
    }

    public bool OpenTodosButtonEnabled { get; private set; } = true;
    public bool OpenExpensesButtonEnabled { get; private set; } = true;
    public bool OpenFilesButtonEnabled { get; private set; } = true;

    public RelayCommand OpenTodosPageCommand => new(OpenTodos, CanExecuteOpenTodos);
    public RelayCommand OpenExpensesPageCommand => new(OpenExpenses, CanExecuteOpenExpenses);
    public RelayCommand OpenFilesPageCommand => new(OpenFiles, CanExecuteOpenFiles);

    private bool CanExecuteOpenTodos() => CurrentPage.Equals(PAGE.NONE) || !CurrentPage.Equals(PAGE.TODOS);
    private bool CanExecuteOpenExpenses() => CurrentPage.Equals(PAGE.NONE) || !CurrentPage.Equals(PAGE.EXPENSES);
    private bool CanExecuteOpenFiles() => CurrentPage.Equals(PAGE.NONE) || !CurrentPage.Equals(PAGE.FILES);
    
    [ObservableProperty] // Generates CurrentPage property
    [NotifyCanExecuteChangedFor(nameof(OpenTodosPageCommand), nameof(OpenExpensesPageCommand), nameof(OpenFilesPageCommand))]
    private PAGE currentPage = PAGE.NONE;
    
    // TODO - Baje nej bi uporabu async Task: https://docs.avaloniaui.net/docs/how-to/mvvm-how-to#async-command
    private void OpenTodos() {
        UpdateCurrentPage(PAGE.TODOS);
    }
    
    private void OpenExpenses() {
        UpdateCurrentPage(PAGE.EXPENSES);
    }

    private void OpenFiles() {
        UpdateCurrentPage(PAGE.FILES);
    }

    private void UpdateCurrentPage(PAGE page) {
        CurrentPage = page;

        switch (CurrentPage) {
            case PAGE.TODOS:
                OpenTodosButtonEnabled = false; 
                OpenExpensesButtonEnabled = OpenFilesButtonEnabled = true; 
                break;
            case PAGE.EXPENSES:
                OpenExpensesButtonEnabled = false;
                OpenTodosButtonEnabled = OpenFilesButtonEnabled = true; 
                break;
            case PAGE.FILES:
                OpenFilesButtonEnabled = false;
                OpenTodosButtonEnabled = OpenExpensesButtonEnabled = true; 
                break;
        }
    }
    
    public void OnStart() {
        //OpenTodos();
    }
}