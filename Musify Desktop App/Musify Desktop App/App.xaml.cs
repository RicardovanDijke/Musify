using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Musify_Desktop_App.Panels.Login;
using Musify_Desktop_App.Service;

namespace Musify_Desktop_App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // delete the startupuri tag from your app.xaml
            base.OnStartup(e);
            //this MainViewModel from your ViewModel project
            // MainWindow = new LoginWindow();
            //MainWindow.Show();
            new SongPlayer();
            MainWindow = new MainWindow(new MainViewModel());
        }
    }
}
