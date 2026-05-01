using System.Linq;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using HomeServer.Client.Interfaces;
using HomeServer.Client.Util;
using HomeServer.Client.ViewModels;
using ClientConstants = HomeServer.Client.Util.Constants;

namespace HomeServer.Client.Views;

public partial class MainWindow : Window {
    public MainWindow() {
        InitializeComponent();
    }

    private void Window_OnClosing(object? sender, WindowClosingEventArgs e) {
        if (DataContext is not MainWindowViewModel VM || !VM.visitedViews.Any()) {
            return;
        }

        foreach (var visitedView in VM.visitedViews) {
            if (visitedView is not IDataViewModel viewModel) {
                continue;
            }

            string? viewModelData = viewModel.GetDataJson();
            if (viewModelData is null) {
                continue;
            }

            Directory.CreateDirectory(ClientConstants.localSaveFolder);
            string path = Path.Combine(ClientConstants.localSaveFolder, viewModel.SaveFileName);
            File.WriteAllText(path, viewModelData);
        }
    }

    private void Window_OnLoaded(object? sender, RoutedEventArgs e) {
        // TODO - Save data in all x_changes.json files in /changes/ folder.
        // TODO - Resolve conflicts in case of multiple changes to the same item. How tho?
        // throw new NotImplementedException();
    }
}