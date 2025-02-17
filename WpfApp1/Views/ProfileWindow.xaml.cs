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
    /// Логика взаимодействия для ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        private readonly User _user;
        private ProfileViewModel _viewModel; // Добавим ViewModel для работы с данными

        public ProfileWindow(User user)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new ProfileViewModel();
            DataContext = _viewModel; // Установка контекста данных для привязки
            LoadUserProfile();
        }

        private void LoadUserProfile()
        {
            // Загрузка данных пользователя и избранных рецептов в ViewModel
            // Пример:
            // _viewModel.FavoriteRecipes = ...; // Получение избранных рецептов для пользователя из базы данных
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Логика для редактирования профиля пользователя
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Закрыть окно профиля
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRecipes = FavoriteRecipesListBox.SelectedItems.Cast<Recipes>().ToList();
            foreach (var recipe in selectedRecipes)
            {
                // Удаление из ViewModel
                _viewModel.RemoveFromFavorites(recipe);
            }
        }
    }
}