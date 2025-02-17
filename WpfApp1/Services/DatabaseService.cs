using Microsoft.Data.Sqlite;
using System.Windows;
using WpfApp1.Models;
namespace WpfApp1.Services

{

    public class DatabaseService
    {
        private string _connectionString = "Data Source=E:\\WpfApp1\\WpfApp1\\basa.db";

        // Метод для получения всех рецептов
        public List<Recipes> GetAllRecipes()
        {
            var recipes = new List<Recipes>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Recipes";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        recipes.Add(new Recipes
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Type = reader.GetString(2),
                            Image = reader.GetString(3),
                            Ingredients = reader.GetString(4),
                            Instructions = reader.GetString(5),
                            IsFavorite = reader.GetBoolean(6)
                        });
                    }
                }
            }
            return recipes;
        }

        // Метод для получения рецептов по имени
        public List<Recipes> GetRecipesByName(string name)
        {
            var recipes = new List<Recipes>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Recipes WHERE Name LIKE @name";
                command.Parameters.AddWithValue("@name", $"%{name}%");

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        recipes.Add(new Recipes
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Type = reader.GetString(2),
                            Image = reader.GetString(3),
                            Ingredients = reader.GetString(4),
                            Instructions = reader.GetString(5),
                            IsFavorite = reader.GetBoolean(6)
                        });
                    }
                }
            }
            return recipes;
        }

        // Метод для получения рецептов по ингредиенту
        public List<Recipes> GetRecipesByIngredient(string ingredient)
        {
            var recipes = new List<Recipes>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Recipes WHERE Ingredients LIKE @ingredient";
                command.Parameters.AddWithValue("@ingredient", $"%{ingredient}%");

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        recipes.Add(new Recipes
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Type = reader.GetString(2),
                            Image = reader.GetString(3),
                            Ingredients = reader.GetString(4),
                            Instructions = reader.GetString(5),
                            IsFavorite = reader.GetBoolean(6)
                        });
                    }
                }
            }
            return recipes;
        }

        // Метод для получения рецептов по типу
        public List<Recipes> GetRecipesByType(string type)
        {
            var recipes = new List<Recipes>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Recipes WHERE Type = @type";
                command.Parameters.AddWithValue("@type", type);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        recipes.Add(new Recipes
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Type = reader.GetString(2),
                            Image = reader.GetString(3),
                            Ingredients = reader.GetString(4),
                            Instructions = reader.GetString(5),
                            IsFavorite = reader.GetBoolean(6)
                        });
                    }
                }
            }
            return recipes;
        }
        public User GetUserByEmail(string email)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM User WHERE Email = @email";
                command.Parameters.AddWithValue("@email", email);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("Пользователь успешно получен.");
                        return new User
                        {
                            Id = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Email = reader.GetString(3),
                            Password = reader.GetString(2)
                        };
                    }
                }
            }

            Console.WriteLine("Пользователь не найден.");
            return null;
        }
       
        

        public bool RegisterUser(User user)
        {
            try
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();

                    // Проверка, существует ли пользователь с таким email
                    var checkCommand = connection.CreateCommand();
                    checkCommand.CommandText = "SELECT COUNT(*) FROM User WHERE Email = @email";
                    checkCommand.Parameters.AddWithValue("@email", user.Email);
                    var count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        return false; // Пользователь уже существует
                    }

                    // Добавление нового пользователя
                    var insertCommand = connection.CreateCommand();
                    insertCommand.CommandText = "INSERT INTO User (Username, Email, Password) VALUES (@username, @email, @password)";
                    insertCommand.Parameters.AddWithValue("@username", user.Username);
                    insertCommand.Parameters.AddWithValue("@email", user.Email);
                    insertCommand.Parameters.AddWithValue("@password", user.Password);
                    insertCommand.ExecuteNonQuery();

                    return true; // Регистрация успешна
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка регистрации: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }



        // Получение избранных рецептов пользователя
        public List<Recipes> GetFavoriteRecipes(int userId)
        {
            try
            {
                var recipes = new List<Recipes>();
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                    SELECT r.* 
                    FROM Recipes r
                    JOIN FavoriteRecipes fr ON r.Id = fr.RecipesId
                    WHERE fr.UsersId = @userId";
                    command.Parameters.AddWithValue("@userId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            recipes.Add(new Recipes
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Type = reader.GetString(2),
                                Image = reader.GetString(3),
                                Ingredients = reader.GetString(4),
                                Instructions = reader.GetString(5)
                            });
                        }
                    }
                }
                return recipes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка получения избранных рецептов: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Recipes>();
            }
        }

        // Проверка, находится ли рецепт в избранном
        public bool IsRecipeInFavorites(int userId, int recipeId)
        {
            try
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT COUNT(*) FROM FavoriteRecipes WHERE UsersId = @userId AND RecipesId = @recipeId";
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@recipeId", recipeId);
                    var count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка проверки рецепта в избранном: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        public List<User> GetAllUsers()
        {
            var users = new List<User>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM User"; // Замените на название вашей таблицы пользователей

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            Id = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Email = reader.GetString(2),
                            Password = reader.GetString(3)
                        });
                    }
                }
            }
            return users;
        }



        // Добавление рецепта в избранное
        public void AddRecipeToFavorites(int userId, int recipeId)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO FavoriteRecipes (UsersId, RecipesId) VALUES (@userId, @recipeId)";
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@recipeId", recipeId);
                command.ExecuteNonQuery();
            }
        }

        // Удаление рецепта из избранного
        public void RemoveRecipeFromFavorites(int userId, int recipeId)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM FavoriteRecipes WHERE UsersId = @userId AND RecipesId = @recipeId";
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@recipeId", recipeId);
                command.ExecuteNonQuery();
            }
        }
    }
}