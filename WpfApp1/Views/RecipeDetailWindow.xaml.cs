using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Models;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для RecipeDetailWindow.xaml
    /// </summary>
    public partial class RecipeDetailWindow : Window
    {
        private readonly RecipeDetailViewModel _viewModel;
        private readonly Recipes _recipe;


        private readonly User _currentUser; // Переменная для хранения текущего рецепта

        // Конструктор с параметрами для передачи текущего пользователя и рецепта
        public RecipeDetailWindow(Recipes recipe, User currentUser)
        {
            InitializeComponent();
            _viewModel = new RecipeDetailViewModel(recipe, currentUser);
            DataContext = _viewModel; // Устанавливаем DataContext на ViewModel
        }

        private void AddToFavoritesButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.AddToFavoritesCommand.CanExecute(null)) // Проверка, может ли команда выполниться
            {
                _viewModel.AddToFavoritesCommand.Execute(null); // Вызов команды
                MessageBox.Show("Рецепт добавлен в избранное!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Закрытие окна
        }
    }
}