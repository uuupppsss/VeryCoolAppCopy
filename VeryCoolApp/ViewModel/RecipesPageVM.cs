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
                service.SelectedRecipe = value;
            }
        }

        public CommandVM AddNewRecipe {  get; set; }
        public CommandVM RemoveRecipe { get; set; }

        public RecipesPageVM()
        {
            service = CookingServise.Instance;
            GetRecipesAsync();
            service.AddRecipeAsync(new Recipe() { Name = "жареные гвозди" });

            AddNewRecipe = new CommandVM(() =>
            {

            });

            RemoveRecipe = new CommandVM(() => 
            { 
            
            });

        }

        private async void GetRecipesAsync()
        {
            Recipes = await service.GetAllRecipesAsync();
        }

        
    }
}
