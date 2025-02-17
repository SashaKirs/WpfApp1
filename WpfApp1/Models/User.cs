using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public virtual ICollection<FavoriteRecipes> FavoriteRecipes { get; set; }
        public User(string username, string password, string email)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;

        }
        public User()
        {
            FavoriteRecipes = new List<FavoriteRecipes>();
        }
        public void AddToFavorites(Recipes recipe)
        {
            FavoriteRecipes.Add(new FavoriteRecipes
            {
                RecipesId = recipe.Id, // Получение Id рецепта
                UsersId = this.Id // Id текущего пользователя
            });
        }
    }
}

