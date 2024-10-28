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
        private CookingServise service;

        public Recipe SelectedRecipe { get; set; }
        public string RecipeName {  get; set; } 
        public string RecipeInctructions {  get; set; } 
        public List<IngredientValueNavigation> IngredientsList { get; set; }

        public OnlyRecipePageVM()
        {
            service = CookingServise.Instance;
            SelectedRecipe=service.SelectedRecipe;

            RecipeName = SelectedRecipe.Name;
            RecipeInctructions = SelectedRecipe.Instruction;
            IngredientsList=SelectedRecipe.Ingredients;
        }
    }
}
