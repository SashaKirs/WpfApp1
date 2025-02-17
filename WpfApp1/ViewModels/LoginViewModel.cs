using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Command;
using WpfApp1.Services;
using WpfApp1.Views;

namespace WpfApp1.ViewModels
{

    public class LoginViewModel : ViewModelBase
    {

        private string _email;
        private string _password;
        private DatabaseService _databaseService;
        private Window _currentWindow;

        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel(DatabaseService databaseService, Window currentWindow)
        {
            _databaseService = databaseService;
            _currentWindow = currentWindow;
            LoginCommand = new RelayCommand(Login);
            RegisterCommand = new RelayCommand(Register);
        }

        private void Login(object obj)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsValidEmail(Email))
            {
                MessageBox.Show("Некорректный формат электронной почты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var user = _databaseService.GetUserByEmail(Email);

            // Отладочный вывод
            Console.WriteLine($"Email: {Email}, Password: {Password}");

            if (user != null)
            {
                Console.WriteLine($"Данные пользователя: {user.Password}, Пароль: {user.Password}");
            }
            //MessageBox.Show($"{user} - {user.Password} =!!!!= {Password}");
            if (user != null && user.Password == Password)
            {
                var mainWindow = new MainWindow(user, _databaseService);
                mainWindow.Show();
                _currentWindow.Close();
            }
            else
            {
                MessageBox.Show("Неверный адрес электронной почты или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Register(object obj)
        {
            OpenRegistrationWindow(); // Вызов метода для открытия окна регистрации
        }

        private void OpenRegistrationWindow()
        {
            var registerWindow = new RegisterWindow(_databaseService); // Предполагая, что у вас есть RegisterWindow
            _currentWindow.Hide(); // Скрываем текущее окно
            registerWindow.Show();
        }

        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}