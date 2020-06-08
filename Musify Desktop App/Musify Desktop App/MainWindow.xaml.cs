using System.ComponentModel;
using System.Windows;

namespace Musify_Desktop_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow(INotifyPropertyChanged viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            Show();
        }
    }

}
