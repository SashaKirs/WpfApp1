using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Command;
using WpfApp1.Models;
using WpfApp1.Services;


namespace WpfApp1.ViewModels
{
    public class RecipeDetailViewModel : ViewModelBase // Предполагается, что ViewModelBase реализует INotifyPropertyChanged
    {
        private readonly User _currentUser;

        public Recipes Recipe { get; private set; }

        public ICommand AddToFavoritesCommand { get; private set; }

        public RecipeDetailViewModel(Recipes recipe, User currentUser)
        {
            Recipe = recipe;
            _currentUser = currentUser;

            AddToFavoritesCommand = new RelayCommand(AddRecipeToFavorites);
        }

        public void AddRecipeToFavorites(object parameter)

        {
            _currentUser.FavoriteRecipes.Add(new FavoriteRecipes
            {
                RecipesId = Recipe.Id,
                UsersId = _currentUser.Id
            });

            SaveFavoriteRecipeToDatabase();


        }
        private void SaveFavoriteRecipeToDatabase()
        {
            DatabaseService databaseService = new DatabaseService();
            databaseService.AddRecipeToFavorites(_currentUser.Id, Recipe.Id);
        }
    }
}