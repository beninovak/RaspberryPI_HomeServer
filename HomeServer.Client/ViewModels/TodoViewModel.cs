using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using HomeServer.Client.Util;
using HomeServer.Core;
using HomeServer.Core.Models;

namespace HomeServer.Client.ViewModels;

public class TodoViewModel : ViewModelBase {
    public ObservableCollection<TodoBySectionsDTO>? TodosBySections { 
        get;
        set {
            field = value;
            OnPropertyChanged();
        }
    } = [];

    public async void AssembleTodos() {
        TodosBySections = await Rufr.Ruf<ObservableCollection<TodoBySectionsDTO>>("todos/");

        if (TodosBySections is null || !TodosBySections.Any()) {
            // Todon't...kva zj? + button to add maybe??
            // TODO - Logger
        }

        // TODO - Should do something in case of no todos/sections?? Maybe require section for todo insert???
    }
    
    public void Search(string searchString) {
        //string results = await Rufr.MakeRequest($"todos/search/{searchString}");
    }
    
    public void TodoCheckUncheck(Todo todo) {
        todo.IsDone = !todo.IsDone;

        JsonContent jsonBody = JsonContent.Create(todo);
        Rufr.Ruf<Todo>($"todos/{todo.Id}", HTTP_METHOD.PATCH, jsonBody);
    }
}