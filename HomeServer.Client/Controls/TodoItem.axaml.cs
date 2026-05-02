using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using HomeServer.Client.Util;
using HomeServer.Core.Models;

namespace HomeServer.Client.Controls;

public partial class TodoItem : UserControl {
    private readonly Rufr rufr = new();

    public static readonly DirectProperty<TodoItem, bool> IsEditingProperty =
        AvaloniaProperty.RegisterDirect<TodoItem, bool>(
            nameof(IsEditing),
            todoItem => todoItem.IsEditing,
            (todoItem, value) => todoItem.IsEditing = value);

    public bool IsEditing {
        get;
        set {
            FocusTextBox();
            SetAndRaise(IsEditingProperty, ref field, value);
        }
    } = false;
    
    public TodoItem() {
        InitializeComponent();
    }
    
    private void Todo_OnEditClick(object? sender, RoutedEventArgs e) {
        IsEditing = true;
    }
    
    private void Todo_OnSaveClick(object? sender, RoutedEventArgs e) {
        IsEditing = false;
    }

    public void TodoCheckUncheck(object? sender, PointerReleasedEventArgs e) {
        if (DataContext is not Todo todo) {
            return;
        }
        todo.IsDone = !todo.IsDone;
        TrySaveChanges(todo); // Hoče da awaitam ampak res ne rabm question mark?
    }
    
    private async Task TrySaveChanges(Todo todo) {
        JsonContent jsonBody = JsonContent.Create(todo);
        (bool isSuccess,  string message) = await rufr.Ruf($"todos/{todo.Id}", HttpMethod.Patch, jsonBody, nameof(Todo));

        if (!isSuccess) {
            Console.WriteLine(message);
            // TODO - Popup with error message, retry button, and log message?
            return;
        }
        
        // Maybe kle success message? Eh..?
    }
    
    private void FocusTextBox() {
        textBox.Focus();
        if (textBox.Text != null) {
            textBox.CaretIndex = textBox.Text.Length;
        }
    }
}