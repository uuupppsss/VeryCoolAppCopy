using System.Collections.ObjectModel;
using VeryCoolApp.Model;

namespace VeryCoolApp;

public partial class IngredientsPage : ContentPage
{
    private CookingDB _cookingDB;

    private ObservableCollection<Ingredient> _ingredients;

    public ObservableCollection<Ingredient> Ingredients
    {
        get => _ingredients;
        set { _ingredients = value; }
    }

    private Ingredient _selectedIngredient;

    public Ingredient SelectedIngredient
    {
        get => _selectedIngredient;
        set { _selectedIngredient = value; }
    }

    public IngredientsPage()
	{
        InitializeComponent();
        _cookingDB = new CookingDB();
        GetIngredientsAsync();
        BindingContext = this;

        Ingredients.Add(new Ingredient() { Name = "�����", Measurement = "��" });
        Ingredients.Add(new Ingredient() { Name = "�����", Measurement = "��" });
        Ingredients.Add(new Ingredient() { Name = "������", Measurement = "�" });
        Ingredients.Add(new Ingredient() { Name = "����", Measurement = "�" });
    }

    private async void GetIngredientsAsync()
    {
        Ingredients = await _cookingDB.GetIngredientsAsync();
    }
}