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
    class LoginViewModel : ViewModelBase
    {
        private UserService _userService;
        public string Username { get; set; }
        public string Password { get; set; }


        public RelayCommand LoginCommand { get; set; }

        public LoginViewModel() { }

        public LoginViewModel(UserService userService)
        {
            _userService = userService;
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
