using System.Windows.Input;
using WpfApp1.Command;
using WpfApp1.Models;
using WpfApp1.Services;


namespace WpfApp1.ViewModels
{

    public class MainViewModel : ViewModelBase
    {
        private readonly DatabaseService _databaseService;

        // Простой пример: добавляем свойство для хранения списка рецептов
        private List<Recipes> _allRecipes;
        public List<Recipes> AllRecipes
        {
            get => _allRecipes;
            set { _allRecipes = value; OnPropertyChanged(); }
        }

        private string _searchName;
        public string SearchName
        {
            get => _searchName;
            set { _searchName = value; OnPropertyChanged(); }
        }

        private string _searchIngredient;
        public string SearchIngredient
        {
            get => _searchIngredient;
            set { _searchIngredient = value; OnPropertyChanged(); }
        }

        private string _selectedType;
        public string SelectedType
        {
            get => _selectedType;
            set { _selectedType = value; OnPropertyChanged(); }
        }

        public ICommand SearchByNameCommand { get; }
        public ICommand SearchByIngredientCommand { get; }
        public ICommand LoadRecipesByTypeCommand { get; }

        public MainViewModel()
        {
            _databaseService = new DatabaseService();
            AllRecipes = _databaseService.GetAllRecipes();

            // Определение команд
            SearchByNameCommand = new RelayCommand(_ => SearchByName());
            SearchByIngredientCommand = new RelayCommand(_ => SearchByIngredient());
            LoadRecipesByTypeCommand = new RelayCommand(LoadRecipesByType);
        }

        private void SearchByName()
        {
            if (!string.IsNullOrWhiteSpace(SearchName))
            {
                AllRecipes = _databaseService.GetRecipesByName(SearchName);
            }
        }

        private void SearchByIngredient()
        {
            if (!string.IsNullOrWhiteSpace(SearchIngredient))
            {
                AllRecipes = _databaseService.GetRecipesByIngredient(SearchIngredient);
            }
        }

        private void LoadRecipesByType(object parameter)
        {
            if (parameter is string type && !string.IsNullOrWhiteSpace(type))
            {
                AllRecipes = _databaseService.GetRecipesByType(type);
            }
        }
    }
}
