using VeryCoolApp.Model;

namespace VeryCoolApp.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            GetRecipesAsync();
        }

        private List<Recipe> _recipes;

        public List<Recipe> Recipes
        {
            get { return _recipes; }
            set { _recipes = value; }
        }

        private List<Ingredient> _ingredients;

        public List<Ingredient> Ingredients
        {
            get { return _ingredients; }
            set { _ingredients = value; }
        }



        private void GetRecipesAsync()
        {

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
