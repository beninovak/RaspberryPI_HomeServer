using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using HomeServer.Client.Controls;
using HomeServer.Client.ViewModels;

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
    }
    
    private void TodoSearchBox_OnSearch(object? sender, EventArgs e) {
        if (VM is not null && sender is SearchBox searchBox) {
            VM.Search(searchBox.Input);
        }
    }
}