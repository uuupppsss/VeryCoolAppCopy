using System.Collections.ObjectModel;
using VeryCoolApp.Model;
using VeryCoolApp.ViewModel;

namespace VeryCoolApp.Pages;

public partial class RecipesPage : ContentPage
{
    private RecipesPageVM vm;
    public RecipesPage()
    {
        InitializeComponent();
        vm=new RecipesPageVM();
        BindingContext=vm;
    }

    private void RecipeTapped(object sender, ItemTappedEventArgs e)
    {
        vm.SelectedRecipeChanged((Recipe)sender);
    }
}