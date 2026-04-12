using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using HomeServer.Client.Controls;
using HomeServer.Core;

namespace HomeServer.Client.ViewModels;

public class TodoViewModel : ViewModelBase {

    private string SearchValue {
        get {
            return "async";
        }
    }

    private ObservableCollection<TodoDTO> Todos {
        get;
        set;
    } = [];
    
    public void TrySearch(string searchString) {
        Console.WriteLine(searchString);
    }

    private void DoSearch() {
        //Console.WriteLine(SearchBox);
    }
}