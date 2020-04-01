using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Musify_Desktop_App.ViewModels
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

            var msg = await stringTask;

            var content = await msg.Content.ReadAsStringAsync();

            var obj = JsonSerializer.Deserialize<Object>(content);
            Console.Write(content);
        }
    }
}
