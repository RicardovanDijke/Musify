using System.Windows;

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
            MainWindow = new MainWindow(new MainViewModel());
        }
    }
}
