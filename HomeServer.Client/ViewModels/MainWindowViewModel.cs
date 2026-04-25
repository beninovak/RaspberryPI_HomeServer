using System.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace HomeServer.Client.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged {
    
    public ViewModelBase CurrentView {
        get;
        set {
            field = value;
            NotifyPropertyChanged();
        }
    } = new TodoViewModel(); // TODO - Make HomeViewModel() default

    [RelayCommand(CanExecute = nameof(CanExecute))]
    private void OpenPage(string page) {
        CurrentView = page switch {
            "HOME"    => new HomeViewModel(),
            "TODO"    => new TodoViewModel(),
            "EXPENSE" => new ExpenseViewModel(),
            "FILE"    => new FileViewModel(),
            _         => CurrentView,
        };
        OpenPageCommand.NotifyCanExecuteChanged();
    }

    private bool CanExecute(string page) => page switch {
        "HOME"     => CurrentView is not HomeViewModel,
        "TODO"     => CurrentView is not TodoViewModel,
        "EXPENSE" => CurrentView is not ExpenseViewModel,
        "FILE"     => CurrentView is not FileViewModel,
        _          => true,
    };
}