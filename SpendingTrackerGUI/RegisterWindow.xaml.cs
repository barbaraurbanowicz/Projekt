using System.Windows;
using SpendingTrackerGUI.ViewModels;

namespace SpendingTrackerGUI;

public partial class RegisterWindow : Window
{
    public RegisterWindow()
    {
        InitializeComponent();        
        DataContext = new RegisterViewModel();

    }

    private void Register(object sender, RoutedEventArgs e)
    {
        DataContext = new RegisterViewModel();
    }

    private void Login(object sender, RoutedEventArgs e)
    {
        DataContext = new LoginViewModel();
    }
}