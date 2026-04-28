using Avalonia.Controls;
using HomeServer.Client.Util;

namespace HomeServer.Client.Views;

public partial class MainWindow : Window {
    public MainWindow() {
        InitializeComponent();
        _ = Rufr.CheckIfIveServer();
    }

    private void Window_OnClosing(object? sender, WindowClosingEventArgs e) {
        // TODO - Save state to save.json
    }
}