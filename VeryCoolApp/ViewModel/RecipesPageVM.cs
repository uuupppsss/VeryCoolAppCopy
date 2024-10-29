using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeryCoolApp.Model;

namespace VeryCoolApp.ViewModel
{
    public class RecipesPageVM:BaseVM
    {
        private CookingServise service;

        private List<Recipe> _recipes;

        public List<Recipe> Recipes
        {
            get => _recipes;
            set { _recipes = value; }
        }

        private Recipe _selectedRecipe;

        public Recipe SelectedRecipe
        {
            get => _selectedRecipe;
            set 
            { 
                _selectedRecipe = value;
            }
        }

        public CommandVM AddNewRecipe {  get; set; }
        public CommandVM RemoveRecipe { get; set; }

        public RecipesPageVM()
        {
            service = CookingServise.Instance;
            //CreateDemoRecipes();
            GetRecipesAsync();
            AddNewRecipe = new CommandVM(async() =>
            {
                await Shell.Current.GoToAsync("AddRecipePage");
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
            });

            RemoveRecipe = new CommandVM(async() => 
            { 
            
            });

        }

        private async void GetRecipesAsync()
        {
            Recipes = await service.GetAllRecipesAsync();
        }

        private async void CreateDemoRecipes()
        {
            await service.AddRecipeAsync(new Recipe() { Name = "жареные гвозди" });
        }

        
    }
}
