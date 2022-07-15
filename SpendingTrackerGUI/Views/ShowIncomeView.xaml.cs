using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Global.Token);
        var response = await client.GetAsync("http://localhost:5001/api/incomes");
        response.EnsureSuccessStatusCode();
        if (response.IsSuccessStatusCode)
        {
            string message = await response.Content.ReadAsStringAsync();
            model = JsonConvert.DeserializeObject<List<Income>>(message);
            View.ItemsSource = model;
        }
        else if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            MessageBox.Show("Unauthorized");
        }
        else
        {
            MessageBox.Show("Failed");
        }
    }
    private async void Delete(object sender, RoutedEventArgs e)
    {
        dynamic content = ((Button) sender).DataContext;
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Global.Token);
        var response = await client.DeleteAsync($"http://localhost:5001/api/incomes/{content.Id}");
        if (response.IsSuccessStatusCode)
        {
            ShowExpenses();
        }
        else if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            MessageBox.Show("Unauthorized");
        }
        else
        {
            MessageBox.Show("Failed");
        }
    }

}