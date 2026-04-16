using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace HomeServer.Client.Controls;

public partial class SearchBox : UserControl {

    public string Placeholder {
        init => InputBox.Watermark = value;
    }

    private string input = string.Empty;
    public string Input { // TODO - Ne rab bit public?
        get => input;
        private set => input = value;
    }

    public event EventHandler? Search;
    //public event EventHandler? ValueChanged;
    
    public SearchBox() {
        InitializeComponent();
    }

    private void InputBox_OnKeyUp(object? sender, KeyEventArgs e) {
        // int keyCode = (int)e.Key; 
        // if (keyCode < 32 && keyCode != 8 && keyCode != 13) return; // Prevent control chars
        
         Input = input + e.KeySymbol;
         //ValueChanged?.Invoke(this, EventArgs.Empty);
    }

    private void SearchButton_OnClick(object? sender, RoutedEventArgs e) {
        Search?.Invoke(this, EventArgs.Empty);
    }
}