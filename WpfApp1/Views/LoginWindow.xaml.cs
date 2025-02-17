using System.Windows;
using System.Windows.Controls;
using WpfApp1.Services;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow(DatabaseService databaseService)
        {
            InitializeComponent();
            DataContext = new LoginViewModel(databaseService, this);
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            var viewModel = DataContext as LoginViewModel;

            if (viewModel != null)
            {
                viewModel.Password = passwordBox.Password;
            }
        }

    }
}
