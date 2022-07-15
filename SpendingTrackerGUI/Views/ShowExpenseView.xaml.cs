using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using SpendingTrackerAPI.Entities;

namespace SpendingTrackerGUI.Views;

public partial class ShowExpenseView : UserControl
{
    public ShowExpenseView()
    {
        InitializeComponent();
        ShowExpenses();
    }

    private async void ShowExpenses()
    {
        List<Expense> model = null;
        HttpClient client = new HttpClient();
        var response = await client.GetAsync("http://localhost:5001/api/expenses");
        response.EnsureSuccessStatusCode();
        if (response.IsSuccessStatusCode)
        {
            string message = await response.Content.ReadAsStringAsync();
            model = JsonConvert.DeserializeObject<List<Expense>>(message);
            View.ItemsSource = model;
        }
    }
    private async void Delete(object sender, RoutedEventArgs e)
    {
        dynamic content = ((Button) sender).DataContext;
        HttpClient client = new HttpClient();
        var response = await client.DeleteAsync($"http://localhost:5001/api/expenses/{content.Id}");
        if (response.IsSuccessStatusCode)
        {
            ShowExpenses();
        }
    }

}