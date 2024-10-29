using VeryCoolApp.Pages;

namespace VeryCoolApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddRecipePage),typeof(AddRecipePage));
            Routing.RegisterRoute(nameof(AddIngridientPage), typeof(AddIngridientPage));
            Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
            Routing.RegisterRoute(nameof(OnlyRecipe), typeof(OnlyRecipe));
            Routing.RegisterRoute(nameof(RecipesPage), typeof(RecipesPage));
            Routing.RegisterRoute(nameof(IngredientsPage), typeof(IngredientsPage));

        }
    }
}
