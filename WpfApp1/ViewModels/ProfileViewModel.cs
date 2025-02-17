using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private ObservableCollection<Recipes> _favoriteRecipes;
        public ObservableCollection<Recipes> FavoriteRecipes
        {
            get => _favoriteRecipes;
            set
            {
                _favoriteRecipes = value;
                OnPropertyChanged(nameof(FavoriteRecipes));
            }
        }

        public ProfileViewModel()
        {
            // Инициализация списка избранных рецептов
            FavoriteRecipes = new ObservableCollection<Recipes>();
        }

        // Метод для добавления рецепта в избранное
        public void AddToFavorites(Recipes recipe)
        {
            if (!FavoriteRecipes.Contains(recipe))
            {
                FavoriteRecipes.Add(recipe);
            }
        }

        // Метод для удаления рецепта из избранного
        public void RemoveFromFavorites(Recipes recipe)
        {
            if (FavoriteRecipes.Contains(recipe))
            {
                FavoriteRecipes.Remove(recipe);
            }
        }
    }
}