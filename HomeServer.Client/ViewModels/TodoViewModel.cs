using System;
using System.Text.Json;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
        TodosBySections = await Rufr.Ruf<ObservableCollection<TodoBySectionsDTO>>("todos/");

        if (TodosBySections is null) {
            // Todon't...kva zj? + button to add maybe??
            // TODO - Logger
        }

        // TODO - Should do something in case of no todos/sections?? Maybe require section for todo insert???
        
        Console.WriteLine("YEP");
    }
    
    public void Search(string searchString) {
        //string results = await Rufr.MakeRequest($"todos/search/{searchString}");
        //return new TodoDTO();

        
    }
}