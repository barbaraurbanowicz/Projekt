using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SpendingTrackerGUI.ViewModels;
using SpendingTrackerGUI.Views;

namespace SpendingTrackerGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddIncomes(object sender, RoutedEventArgs e)
        {
            DataContext = new IncomeViewModel();
        }

        private void ShowIncomes(object sender, RoutedEventArgs e)
        {
            DataContext = new ShowIncomeViewModel();
        }

        private void AddExpenses(object sender, RoutedEventArgs e)
        {
            DataContext = new ExpenseViewModel();
        }

        private void ShowExpenses(object sender, RoutedEventArgs e)
        {
            DataContext = new ShowExpenseViewModel();
        }

        private void Main(object sender, RoutedEventArgs e)
        {
            DataContext = new MainViewModel();
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            DataContext = new LoginViewModel();
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            DataContext = new RegisterViewModel();
        }
    }
}