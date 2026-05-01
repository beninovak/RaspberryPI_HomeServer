using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using HomeServer.Client.Interfaces;
using HomeServer.Client.Util;
using HomeServer.Core.Models;

namespace HomeServer.Client.ViewModels;

public class TodoViewModel : ViewModelBase, IDataViewModel {
    public ObservableCollection<TodoBySectionsDTO>? TodosBySections { 
        get;
        set {
            field = value;
            OnPropertyChanged();
        }
    } = [];

    private readonly Rufr rufr = new();

    public async void AssembleTodos() {
        try {
            TodosBySections = await rufr.Ruf<ObservableCollection<TodoBySectionsDTO>>("todos/", this);
        } catch (Exception e) {
            // TODO - Pokaž window popup na sceno "zgodiu se je gugugaga". Maybe ma popup retry button?
        }
    }
    
    public void Search(string searchString) {
        
    }
    
    public async Task TodoCheckUncheck(Todo todo) {
        todo.IsDone = !todo.IsDone;
        await TrySaveChanges(todo);
    }
    
    private async Task TrySaveChanges(Todo todo) {
        JsonContent jsonBody = JsonContent.Create(todo);
        (bool isSuccess,  string message) = await rufr.Ruf($"todos/{todo.Id}", HTTP_METHOD.PATCH, jsonBody, this);

        if (!isSuccess) {
            Console.WriteLine(message);
            // TODO - Popup with error message, retry button, and log message?
            return;
        }
        
        // Maybe kle success message? Eh..?
    }
    
    
    public string SaveFileName { get; init; } = "todo_save.json";
    public string ChangesFileName { get; init; } = "todo_changes.json";

    public string? GetDataJson() {
        if (TodosBySections is null || !TodosBySections.Any()) {
            return null;
        }
        return JsonSerializer.Serialize(TodosBySections);
    }
}