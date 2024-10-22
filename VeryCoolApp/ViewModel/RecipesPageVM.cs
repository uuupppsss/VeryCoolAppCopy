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
            set 
            { 
                _selectedRecipe = value;
                _cookingDB.SelectedRecipe = value;
            }
        }

        public CommandVM AddNewRecipe {  get; set; }
        public CommandVM RemoveRecipe { get; set; }
        public CommandVM SeeRecipe { get; set; }

        public RecipesPageVM()
        {
            _cookingDB = new CookingDB();
            GetRecipesAsync();
            Recipes.Add(new Recipe() { Name = "жареные шмели" });
            Recipes.Add(new Recipe() { Name = "гвозди в кляре" });

            AddNewRecipe = new CommandVM(() =>
            {

            });

            RemoveRecipe = new CommandVM(() => 
            { 
            
            });

            SeeRecipe = new CommandVM(() => 
            {
            
            });
        }

        private async void GetRecipesAsync()
        {
            Recipes = await _cookingDB.GetRecipesAsync();
        }
    }
}
