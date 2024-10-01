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
            get { return _recipes; }
            set { _recipes = value; }
        }

        private ObservableCollection<Ingredient> _ingredients;

        public ObservableCollection<Ingredient> Ingredients
        {
            get { return _ingredients; }
            set { _ingredients = value; }
        }

        public MainPage()
        {
            InitializeComponent();
            GetRecipesAsync();
            _cookingDB = new CookingDB();
            Recipes=_cookingDB.Recipes;
            Ingredients=_cookingDB.Ingredients;
        }

        private void GetRecipesAsync()
        {
           Recipes = new ObservableCollection<Recipe>();
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
