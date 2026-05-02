using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using HomeServer.Client.Controls;
using HomeServer.Client.ViewModels;
using HomeServer.Core;
using HomeServer.Core.Models;

namespace HomeServer.Client.Views;

public partial class TodoView : UserControl {
    private TodoViewModel? VM;

    public TodoView() {
        InitializeComponent();
    }
    
    private void TodoView_OnLoaded(object? sender, RoutedEventArgs e) {
        VM = DataContext as TodoViewModel;
        if (VM is null) {
            throw new Exception("TodoViewModel is null. Fix your code buddy!");
        }
    }
    
    // TODO - Poglej če se to da nekak prestaut u SearchBox alpa TodoViewModel...
    private void TodoSearchBox_OnSearch(object? sender, EventArgs e) {
        if (sender is not SearchBox searchBox) {
            // TODO - Log: "Yo buddy, the fuck? A SearchBox didn't call this. You done fucked up..."
            // A se sploh loh zgodi tho? Hja...
            return;
        }
        VM!.Search(searchBox.Input);
    }
}