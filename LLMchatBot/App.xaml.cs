using System.Windows;
using GeminiChatBot.ViewModels;
using GeminiChatBot.Views;

namespace GeminiChatBot;

public partial class App : Application
{
    private void App_OnStartup(object sender, StartupEventArgs e)
    {
        // MainWindow'a ViewModel'i bagla
        var mainWindow = new MainWindow();
        mainWindow.DataContext = new MainViewModel();
        mainWindow.Show();
    }
}

