using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using SpendingTrackerAPI.Entities;

namespace SpendingTrackerGUI.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        TotalExpenes();
        TotalIncomes();
    }


    private async void TotalExpenes()
    {
        List<Expense> model = null;
        int sum = 0;
        HttpClient client = new HttpClient();
        var response = await client.GetAsync("https://localhost:5001/api/expenses");
        response.EnsureSuccessStatusCode();
        if (response.IsSuccessStatusCode)
        {
            string message = await response.Content.ReadAsStringAsync();
            model = JsonConvert.DeserializeObject<List<Expense>>(message);
        }
        
        foreach(var income in model)
        {
            sum += income.Amount;
        }

        totalExpenses.Text = sum.ToString();
    }
    private async void TotalIncomes()
    {
        List<Income> model = null;
        int sum = 0;
        HttpClient client = new HttpClient();
        var response = await client.GetAsync("https://localhost:5001/api/incomes");
        response.EnsureSuccessStatusCode();
        if (response.IsSuccessStatusCode)
        {
            string message = await response.Content.ReadAsStringAsync();
            model = JsonConvert.DeserializeObject<List<Income>>(message);
        }
        
        foreach(var income in model)
        {
            sum += income.Amount;
        }

        totalIncomes.Text = sum.ToString();
    }
}