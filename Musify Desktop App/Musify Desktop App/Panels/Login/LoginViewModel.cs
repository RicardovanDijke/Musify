using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Musify_Desktop_App.Model;
using Musify_Desktop_App.Service;

namespace Musify_Desktop_App.Panels.Login
{
    internal class LoginViewModel : ViewModelBase
    {
        private readonly UserService _userService;
        public string Username { get; set; }
        public string Password { get; set; }


        public RelayCommand LoginCommand { get; set; }

        public LoginViewModel() { }

        public LoginViewModel(UserService userService)
        {
            _userService = userService;
            Username = "Musify";
            Password = "musify";

            LoginCommand = new RelayCommand(Login, CanLogin);
        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        private void Login()
        {
            var user = _userService.Login(Username, Password);
            if (user != null)
            {
                GoToMainScreen(user);
            }
        }

        private void GoToMainScreen(User user)
        {
            var mainWindow = new MainWindow(new MainViewModel(user));
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
