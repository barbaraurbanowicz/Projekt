using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using SpendingTrackerAPI.Entities;

namespace SpendingTrackerGUI.Views;

public partial class ShowIncomeView : UserControl
{
    public ShowIncomeView()
    {
        InitializeComponent();
        ShowExpenses();
    }

    private async void ShowExpenses()
    {
        List<Income> model = null;
        HttpClient client = new HttpClient();
        var response = await client.GetAsync("https://localhost:5001/api/incomes");
        response.EnsureSuccessStatusCode();
        if (response.IsSuccessStatusCode)
        {
            string message = await response.Content.ReadAsStringAsync();
            model = JsonConvert.DeserializeObject<List<Income>>(message);
            View.ItemsSource = model;
        }
    }
    private async void Delete(object sender, RoutedEventArgs e)
    {
        dynamic content = ((Button) sender).DataContext;
        HttpClient client = new HttpClient();
        var response = await client.DeleteAsync($"https://localhost:5001/api/incomes/{content.Id}");
        if (response.IsSuccessStatusCode)
        {
            ShowExpenses();
        }
    }

    // private void Update(object sender, RoutedEventArgs e)
    // {
    //     throw new System.NotImplementedException();
    // }
}