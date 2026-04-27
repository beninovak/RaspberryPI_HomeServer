using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using HomeServer.Client.Controls;
using HomeServer.Client.ViewModels;
using HomeServer.Core;
using HomeServer.Core.Models;

namespace HomeServer.Client.Views;

public partial class TodoView : UserControl {
    public TodoView() {
        InitializeComponent();
    }
    
    private TodoViewModel? VM;
    private void TodoView_OnLoaded(object? sender, RoutedEventArgs e) {
        VM = DataContext as TodoViewModel;
        if (VM is null) {
            throw new Exception("TodoViewModel is null. Fix your code buddy!");
        }

        VM.AssembleTodos();
    }
    
    private void TodoSearchBox_OnSearch(object? sender, EventArgs e) {
        if (VM is not null && sender is SearchBox searchBox) {
            VM.Search(searchBox.Input);
            return;
        }
        
        throw new Exception("Yo buddy, the fuck? Either a SearchBox didn't call this, or the ViewModel doesn't exist. You done fucked up...");
    }

    private void TodoCheckUncheck(object sender, PointerReleasedEventArgs e) {
        e.Handled = true;
        if (VM is not null && sender is Grid { DataContext: Todo todo }) {
            VM.TodoCheckUncheck(todo);
            return;
        }

        throw new Exception("Yo buddy, the fuck? Either a Grid didn't call this, or it doesn't have a Todo context. You done fucked up...");
    }
}