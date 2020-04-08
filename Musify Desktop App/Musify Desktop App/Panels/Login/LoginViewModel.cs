using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Musify_Desktop_App.Panels.Login
{
    class LoginViewModel : ViewModelBase
    {

        public string Username { get; set; }
        public string Password { get; set; }


        public RelayCommand LoginCommand { get; set; }
        public LoginViewModel()
        {
            Username = "Ricardo1184";
            Password = "password";

            LoginCommand = new RelayCommand(Login, CanLogin);
        }

        private bool CanLogin()
        {
            return !String.IsNullOrEmpty(Username) && !String.IsNullOrEmpty(Password);
        }

        private async void Login()
        {
            //todo move to LoginService
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            string payload = JsonSerializer.Serialize(new
            {
                username = Username,
                password = Password,
            });

            var stringTask = client.PostAsync("https://localhost:44321/api/login", new StringContent(payload, Encoding.UTF8, "application/json"));

            try
            {
                var msg = await stringTask;
                var content = await msg.Content.ReadAsStringAsync();

                //var obj = JsonSerializer.Deserialize<object>(content);
                Console.Write(content);

                GoToMainScreen();

               
            }
            catch
            {
                //todo display LoginUnsuccesfull message
                GoToMainScreen();
            }
        }

        private void GoToMainScreen()
        {
            var mainWindow = new MainWindow();
            //window.Close();
            mainWindow.Show();

            //close current screen from viewmodel
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this) item.Close();
            }
        }
    }
}
