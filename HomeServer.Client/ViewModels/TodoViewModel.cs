using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HomeServer.Client.Util;
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
    
    public async Task<TodoDTO> Search(string searchString) {
        Console.WriteLine(searchString);
        string results = await Rufr.MakeRequest($"todos/search/{searchString}");


        return new TodoDTO();
    }
}