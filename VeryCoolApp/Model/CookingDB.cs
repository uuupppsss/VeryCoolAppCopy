using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeryCoolApp.Model
{
    public class CookingDB
    {
        private ObservableCollection<Recipe> _recipes;
        private ObservableCollection<Ingredient> _ingredients;
        private int _recipeIdCounter = 1;
        private int _ingredientIdCounter = 1;

        public CookingDB()
        {
            _recipes = new ObservableCollection<Recipe>();
            _ingredients = new ObservableCollection<Ingredient>();
        }
        //получение списка рецептов
        public async Task<ObservableCollection<Recipe>> GetRecipesAsync()
        {
            return await Task.FromResult( new ObservableCollection<Recipe>( _recipes) );
        }

        //вывод рецепта по айди
        public async Task<Recipe> GetRecipeByIDAsync(int ID)
        {
            Recipe selectedRecipe= _recipes.FirstOrDefault(r => r.Id == ID);
            Recipe newRecipe = new Recipe
            {
                Id = selectedRecipe.Id,
                Name = selectedRecipe.Name, 
                Instruction = selectedRecipe.Instruction,
                Ingredients = selectedRecipe.Ingredients
            };
            
            return await Task.FromResult(newRecipe);
        }

        //добавление рецепта
        public async Task AddRecipeAsync(Recipe recipe)
        { 
            recipe.Id = _recipeIdCounter++;
            
        }

    }

}
