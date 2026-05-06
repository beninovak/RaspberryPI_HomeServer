using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using HomeServer.Client.Interfaces;
using HomeServer.Client.Util;
using HomeServer.Core.Models;

namespace HomeServer.Client.ViewModels;

public class TodoViewModel : ViewModelBase, IDataViewModel {
   
    public ObservableCollection<TodoByCategoryDTO>? TodosBySections { 
        get;
        set {
            field = value;
            OnPropertyChanged();
        }
    } = [];

    private readonly Rufr rufr = new();

    public TodoViewModel() {
        AssembleTodos();
    }
    
    private async void AssembleTodos() {
        try {
            TodosBySections = await rufr.Ruf<ObservableCollection<TodoByCategoryDTO>>("todos/", nameof(Todo));
        } catch (Exception e) {
            // TODO - Pokaž window popup na sceno "zgodiu se je gugugaga". Maybe ma popup retry button?
        }
    }
    
    public void Search(string searchString) {
        
    }
    
    public string ModelName => nameof(Todo);

    public string? GetDataJson() {
        if (TodosBySections is null || !TodosBySections.Any()) {
            return null;
        }
        return JsonSerializer.Serialize(TodosBySections);
    }
}