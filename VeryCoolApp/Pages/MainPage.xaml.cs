using System.Collections.ObjectModel;
using VeryCoolApp.Model;

namespace VeryCoolApp.Pages
{
    public partial class MainPage : ContentPage
    {
        private CookingDB _cookingDB;

        private ObservableCollection<Recipe> _recipes;

        public ObservableCollection<Recipe> Recipes
        {
            get => _recipes; 
            set { _recipes = value; }
        }

        private Recipe _selectedRecipe;

        public Recipe SelectedRecipe
        {
            get => _selectedRecipe; 
            set { _selectedRecipe = value; }
        }


        private ObservableCollection<Ingredient> _ingredients;

        public ObservableCollection<Ingredient> Ingredients
        {
            get => _ingredients;
            set { _ingredients = value; }
        }

        public MainPage()
        {
            InitializeComponent();
            _cookingDB = new CookingDB();
            GetRecipesAsync();
            BindingContext = this;
        }

        private async void GetRecipesAsync()
        {
           Recipes = await _cookingDB.GetRecipesAsync();
        }

        private async void GetIngredientsAsync()
        {
            Ingredients = await _cookingDB.GetIngredientsAsync();
        }

        private void OnAddRecipeClicked(object sender, EventArgs e)
        {
            AddRecipePage page = new AddRecipePage();
        }

        private void OnRecipeSelected(object sender, ItemTappedEventArgs e)
        {

        }
    }

}
