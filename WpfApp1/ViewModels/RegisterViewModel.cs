using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Command;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views;

namespace WpfApp1.ViewModels
{


    public class RegisterViewModel : ViewModelBase
    {
        private string _name;
        private string _email;
        private string _password;
        private string _confirmPassword;
        private DatabaseService _databaseService;
        private Window _currentWindow;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set { _confirmPassword = value; OnPropertyChanged(); }
        }

        public ICommand RegisterCommand { get; }

        public RegisterViewModel(DatabaseService databaseService, Window currentWindow)
        {
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
            _currentWindow = currentWindow ?? throw new ArgumentNullException(nameof(currentWindow));
            RegisterCommand = new RelayCommand(_ => Register());
        }

        private void Register()
        {
            // Проверка на корректность вводимых данных
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                MessageBox.Show("Все поля должны быть заполнены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Password != ConfirmPassword)
            {
                MessageBox.Show("Пароли не совпадают.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsValidEmail(Email))
            {
                MessageBox.Show("Некорректный формат электронной почты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка, существует ли пользователь с таким email
            User existingUser = _databaseService.GetUserByEmail(Email);
            if (existingUser != null)
            {
                MessageBox.Show("Пользователь с данным email уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var user = new User {
               Username = Name,
                Email = Email,
           Password = Password
           };
            if (_databaseService.RegisterUser(user))
            {
                MessageBox.Show("Регистрация успешна!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                var loginWindow = new LoginWindow(_databaseService);
                loginWindow.Show();
                _currentWindow.Close();
            }
            else
            {
                MessageBox.Show("Ошибка регистрации. Повторите попытку.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
