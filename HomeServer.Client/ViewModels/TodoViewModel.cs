using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using HomeServer.Client.Interfaces;
using HomeServer.Client.Util;
using HomeServer.Core.DTOs;
using HomeServer.Core.DTOs.Todos;
using HomeServer.Core.Models;
using HomeServer.Core.Models.Todos;

namespace HomeServer.Client.ViewModels;

public class TodoViewModel : ViewModelBase, IDataViewModel {
   
    public ObservableCollection<TodoCategoryReadDTO>? TodosByCategory { 
        get;
        set {
            field = value;
            OnPropertyChanged();
        }
    } = [];

    private readonly Rufr rufr = new();

    public async Task AssembleTodosAsync() {
        try {
            TodosByCategory = await rufr.RufAsync<ObservableCollection<TodoCategoryReadDTO>>("todos/", nameof(Todo));
        } catch (Exception e) {
            // TODO - Pokaž window popup na sceno "zgodiu se je gugugaga". Maybe ma popup retry button?
        }
    }
    
    public void Search(string searchString) {
        
    }
    
    public string ModelName => nameof(Todo);

    public string? GetDataJson() {
        if (TodosByCategory is null || !TodosByCategory.Any()) {
            return null;
        }
        return JsonSerializer.Serialize(TodosByCategory);
    }
}