using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CommunityToolkit.Mvvm.Input;
using HomeServer.Client.Interfaces;

namespace HomeServer.Client.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged {
    public readonly List<ViewModelBase> visitedViews = [];
    
    public ViewModelBase CurrentView {
        get;
        set {
            field = value;
            NotifyPropertyChanged();
        }
    } = new TodoViewModel(); // TODO - Make HomeViewModel() default


    public MainWindowViewModel() {
        visitedViews.Add(CurrentView);
    }

    [RelayCommand(CanExecute = nameof(CanExecute))]
    private void OpenPage(string page) {
        // TODO - Return view from visitedViews if available, otherwise create new.
        CurrentView = page switch {
            "HOME"    => visitedViews.FirstOrDefault(vm => vm is HomeViewModel, new HomeViewModel()),
            "TODO"    => visitedViews.FirstOrDefault(vm => vm is TodoViewModel, new TodoViewModel()),
            "EXPENSE" => visitedViews.FirstOrDefault(vm => vm is ExpenseViewModel, new ExpenseViewModel()), 
            "FILE"    => visitedViews.FirstOrDefault(vm => vm is FileViewModel, new FileViewModel()),
            _ => CurrentView,
        };
        OpenPageCommand.NotifyCanExecuteChanged();

        if (CurrentView is IDataViewModel && visitedViews.All(vm => vm.GetType() != CurrentView.GetType())) {
            visitedViews.Add(CurrentView);
        }
    }

    private bool CanExecute(string page) => page switch {
        "HOME"     => CurrentView is not HomeViewModel,
        "TODO"     => CurrentView is not TodoViewModel,
        "EXPENSE"  => CurrentView is not ExpenseViewModel,
        "FILE"     => CurrentView is not FileViewModel,
        _          => true,
    };
}