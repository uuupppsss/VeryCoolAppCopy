using System.Collections.ObjectModel;
using VeryCoolApp.Model;

namespace VeryCoolApp;

public partial class RecipesPage : ContentPage
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

    public RecipesPage()
    {
        InitializeComponent();
        _cookingDB = new CookingDB();
        GetRecipesAsync();
        Recipes.Add(new Recipe() { Name = "������� �����" });
        Recipes.Add(new Recipe() { Name = "������ � �����" });
        BindingContext = this;
    }

    private async void GetRecipesAsync()
    {
        Recipes = await _cookingDB.GetRecipesAsync();
    }

   
}