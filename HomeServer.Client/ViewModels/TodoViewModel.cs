using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Input;
using HomeServer.Client.Util;
using HomeServer.Core;

namespace HomeServer.Client.ViewModels;

public class TodoViewModel : ViewModelBase {
    //public TodoViewModel() { }

    public ObservableCollection<TodoBySectionsDTO>? TodosBySections { 
        get;
        set {
            field = value;
            OnPropertyChanged();
        }
    } = [];

    public async void AssembleTodos() {
        TodosBySections = await Rufr.Ruf<ObservableCollection<TodoBySectionsDTO>>("todos/", HTTP_METHOD.GET);

        if (TodosBySections is null || !TodosBySections.Any()) {
            // Todon't...kva zj? + button to add maybe??
            // TODO - Logger
        }

        // TODO - Should do something in case of no todos/sections?? Maybe require section for todo insert???
    }
    
    public void Search(string searchString) {
        //string results = await Rufr.MakeRequest($"todos/search/{searchString}");
        //return new TodoDTO();
    }

    
    public async void TodoCheckUncheck(uint id) {
        
        // TODO - Continue here...pogruntej kašn type rabm dt Rufr.Ruf
        Task? todoDTO = await Rufr.Ruf<Task>($"todos/{id}/status", HTTP_METHOD.PATCH);

        if (todoDTO is null) {
            // TODO - Maybe display X/fail message at the top of screen?
            return;
        }

        foreach (var section in TodosBySections) {
            try {
                TodoDTO temp = section.Todos.First(todo => todo.Id == id);
                temp.IsDone = !temp.IsDone;
            } catch {}
        }
        // TODO - Maybe display checkmark at the top of screen?
    }
}