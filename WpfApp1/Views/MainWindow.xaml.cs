using System.Windows;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User _user;
        private DatabaseService _databaseService;

        public MainWindow(User user, DatabaseService databaseService)
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            _user = user;
            _databaseService = databaseService; // Используйте здесь DatabaseService
            Title = "Добро пожаловать, " + _user.Username; // Например, установка заголовка окна
        }

        private void WatermarkTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

       
        
    }
}