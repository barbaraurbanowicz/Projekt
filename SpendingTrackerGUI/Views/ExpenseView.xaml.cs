using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using SpendingTrackerAPI.Entities;

namespace SpendingTrackerGUI.Views;

public partial class ExpenseView : UserControl
{
    public  ExpenseView()
    {
        InitializeComponent();
        ShowCategories();
        

    }

    private async void ShowCategories()
    {
        List<ExpenseCategory> model = null;
        HttpClient client = new HttpClient();


        var response = await client.GetAsync("https://localhost:5001/api/expense/categories");
        response.EnsureSuccessStatusCode();
        if (response.IsSuccessStatusCode)
        {
            string message = await response.Content.ReadAsStringAsync();
            model = JsonConvert.DeserializeObject<List<ExpenseCategory>>(message);
            View.ItemsSource = model;

        }
    }
    
    
    private void AddExpense(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void AddCategory(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void Delete(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}