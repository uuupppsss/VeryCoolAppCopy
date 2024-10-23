using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VeryCoolApp.Model;

namespace VeryCoolApp.ViewModel
{
    public class OnlyRecipePageVM:BaseVM
    {
        private readonly CookingDB _cookingDB;

        public Recipe SelectedRecipe { get; set; }
        public string RecipeName {  get; set; } 
        public string RecipeInctructions {  get; set; } 
        public List<Ingredient> IngredientsList { get; set; }

        public OnlyRecipePageVM()
        {
            _cookingDB = new CookingDB();
            SelectedRecipe=_cookingDB.SelectedRecipe;

            RecipeName = SelectedRecipe.Name;
            RecipeInctructions = SelectedRecipe.Instruction;
            IngredientsList=SelectedRecipe.Ingredients;
        }
    }
}
