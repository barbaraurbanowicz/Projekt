using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using SpendingTrackerAPI.DTOModels;
using SpendingTrackerAPI.Entities;

namespace SpendingTrackerGUI.Views;

public partial class IncomeView : UserControl
{
    public IncomeView()
    {
        InitializeComponent();
        ShowCategories();
    }

    private async void ShowCategories()
    {
        List<IncomeCategory> model = null;
        HttpClient client = new HttpClient();
        var response = await client.GetAsync("https://localhost:5001/api/income/categories");
        response.EnsureSuccessStatusCode();
        if (response.IsSuccessStatusCode)
        {
            string message = await response.Content.ReadAsStringAsync();
            model = JsonConvert.DeserializeObject<List<IncomeCategory>>(message);
            View.ItemsSource = model;
        }
    }
    
    private async void AddIncome(object sender, RoutedEventArgs e)
    {
        if (Name.Text != "" && Amount.Text != "" && Category.Text != "")
        {
            HttpClient client = new HttpClient();
            string json = JsonConvert.SerializeObject( new CreateIncomeDTO() {Name = Name.Text, Amount = Int32.Parse(Amount.Text) , IncomeCategoryId = Int32.Parse(Category.Text)});
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:5001/api/incomes", stringContent);
            if (response.IsSuccessStatusCode)
            {
                ShowCategories();
                Name.Text = "";
                Amount.Text = "";
                Category.Text = "";
            }
            else
            {
                MessageBox.Show("Failed");
            }  
        }
        else
        {
            MessageBox.Show("Complete form");
        }
    }

    private async void AddCategory(object sender, RoutedEventArgs e)
    {
        HttpClient client = new HttpClient();
        string json = JsonConvert.SerializeObject( new CreateIncomeCategoryDTO() {Name = NameCategory.Text});
        var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://localhost:5001/api/income/categories", stringContent);
        if (response.IsSuccessStatusCode)
        {
            ShowCategories();
            NameCategory.Text = "";
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
        var response = await client.DeleteAsync($"https://localhost:5001/api/income/categories/{content.Id}");
        if (response.IsSuccessStatusCode)
        {
            ShowCategories();
        }
    }
}