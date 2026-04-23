using System.Collections.ObjectModel;
using System.Linq;
using HomeServer.Client.Util;
using HomeServer.Core;

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
        bool isSuccess = await Rufr.Ruf($"todos/{id}/status", HTTP_METHOD.PATCH);

        if (!isSuccess) {
            // TODO - Maybe display checkmark at the top of screen?
            return;
        }

        if (TodosBySections is null) {
            // TODO - Maybe display checkmark at the top of screen?
            return;
        }
        
        // TODO - Continue here: treba pogruntat kako se spremeni checkbox na FE...ig ne u foreach???
        foreach (TodoDTO todo in TodosBySections.SelectMany(section => section.Todos)) {
            if (todo.Id != id) continue;
            
            var todoDto = todo;
            todoDto.IsDone = !todoDto.IsDone;
            return;
        }
    }
}