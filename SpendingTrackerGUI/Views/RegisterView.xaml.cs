using System;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using SpendingTrackerAPI.DTOModels;

namespace SpendingTrackerGUI.Views;

public partial class RegisterView : UserControl
{
    public RegisterView()
    {
        InitializeComponent();
    }

    private async void Register_Click(object sender, RoutedEventArgs e)
    {
        if (Email.Text != "" && Password.Password != "" )
        {
            HttpClient client = new HttpClient();
            string json = JsonConvert.SerializeObject( new CreateUserDTO() {Email = Email.Text, Password = Password.Password});
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:5001/api/users/register", stringContent);
            if (response.IsSuccessStatusCode)
            {
                
                Email.Text = "";
                Password.Password = "";
                MessageBox.Show("Success Register!");
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
}