using System.Net.Http;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using SpendingTrackerAPI.DTOModels;

namespace SpendingTrackerGUI.Views;

public partial class LoginView : UserControl
{
    public LoginView()
    {
        InitializeComponent();
    }

    private async void Login_Click(object sender, RoutedEventArgs e)
    {
        if (Email.Text != "" && Password.Password != "" )
        {
            HttpClient client = new HttpClient();
            string json = JsonConvert.SerializeObject( new CreateUserDTO() {Email = Email.Text, Password = Password.Password});
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:5001/api/users/login", stringContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                Email.Text = "";
                Password.Password = "";
                Global.Token = result;
                MessageBox.Show("Login Success!");
                RegisterWindow window = new RegisterWindow();
                window.Close();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

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