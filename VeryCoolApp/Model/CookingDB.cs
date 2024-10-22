using __XamlGeneratedCode__;
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

        private readonly DB_Context _dbContext;

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

        //нахождение рецепта по айди
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
            _recipes.Add(recipe);
        }

        //добавление ингридиента
        public async Task AddIngredientAsync(Ingredient ingredient)
        {
            ingredient.Id = _ingredientIdCounter++;
            _ingredients.Add(ingredient);
        }

        //нахождение ингридиента по айди
        public async Task<Ingredient> GetIngredientByIDAsync(int id)
        {
            Ingredient selectedIngredient= _ingredients.FirstOrDefault(i => i.Id == id);
            Ingredient newIngredient = new Ingredient()
            {
                Id=selectedIngredient.Id,
                Name=selectedIngredient.Name,
                Measurement = selectedIngredient.Measurement
            };

            return await Task.FromResult(newIngredient);
        }

        private async Task<Recipe> GetRecipe(int id)
            => await Task.FromResult(_recipes.FirstOrDefault(r => r.Id == id));

        private async Task<Ingredient> GetIngredient(int id)
            => await Task.FromResult(_ingredients.FirstOrDefault(i=>i.Id==id));


        //удаление рецепта по айди
        public async Task RemoveRecipeById(int id)
        {
            var recipe = await GetRecipe(id);
            if (recipe != null) 
                _recipes.Remove(recipe);
        }

        //удаление ингридиента по айди
        public async Task RemoveIngredientById(int id)
        {
            var ingredient = await GetIngredient(id);
            if(ingredient!=null)
                _ingredients.Remove(ingredient);
        }
        //редактирование рецепта
        public async Task EditRecipe(Recipe UpdatingRecipe)
        {
            var recipe = await GetRecipe(UpdatingRecipe.Id);
            if (recipe!=null)
            {
                recipe.Name= UpdatingRecipe.Name;
                recipe.Ingredients=UpdatingRecipe.Ingredients;
                recipe.Instruction=UpdatingRecipe.Instruction;
                
            }
        }
        //редактирование ингридиента
        public async Task EditIngridient(Ingredient UpdatingIngredient)
        {
            var ingredient = await GetIngredient(UpdatingIngredient.Id);
            if (ingredient!=null)
            {
                ingredient.Name= UpdatingIngredient.Name;
                ingredient.Measurement=UpdatingIngredient.Measurement;
            }
        }
        
        //получение списка ингридиентов
        public async Task<ObservableCollection<Ingredient>> GetIngredientsAsync()
        {
            return await Task.FromResult(new ObservableCollection<Ingredient>(_ingredients));
        }
       
        //Получение ингридиента по айди
        public async Task<Ingredient> GetIngredientByIdAsync(int id)
        {
            return await Task.FromResult(_ingredients.FirstOrDefault(i => i.Id == id));
        }
    }

}
