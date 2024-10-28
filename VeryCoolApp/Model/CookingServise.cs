

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Windows.System;

namespace VeryCoolApp.Model
{
    public class CookingServise
    {
        public User CurrentUser { get; set; }
        public Recipe SelectedRecipe { get; set; }
        public Ingredient SelectedIngredient { get; set; }
        private CookingDB context;

        static CookingServise instance;

        public static CookingServise Instance
        {
            get
            {
                if (instance == null)
                    instance = new CookingServise();
                return instance;
            }
        }

        public CookingServise()
        {
            context = new CookingDB("CookingServise");
 
        }

        // Методы для работы с Ingredient

        public async Task AddIngredientAsync(Ingredient ingredient)
        {
            context.Ingredients.Add(ingredient);
            await context.SaveChangesAsync();
        }

        public async Task<List<Ingredient>> GetAllIngredientsAsync()
        {
            List<Ingredient> ingredients= await context.Ingredients.ToListAsync();
            return new List<Ingredient>(ingredients);
        }

        public async Task<Ingredient> GetIngredientByIdAsync(int id)
        {
            Ingredient ingredient = await context.Ingredients.FindAsync(id);
            return new Ingredient()
            {
                Id=ingredient.Id, Name=ingredient.Name,Measurement=ingredient.Measurement
            };
        }

        public async Task UpdateIngredientAsync(Ingredient ingredient)
        {
            context.Ingredients.Update(ingredient);
            await context.SaveChangesAsync();
        }

        public async Task DeleteIngredientAsync(int id)
        {
            var ingredient = await context.Ingredients.FindAsync(id);
            if (ingredient != null)
            {
                context.Ingredients.Remove(ingredient);
                await context.SaveChangesAsync();
            }
        }

        // Методы для работы с Recipe
        public async Task AddRecipeAsync(Recipe recipe)
        {
            context.Recipes.Add(recipe);
            await context.SaveChangesAsync();
        }

        public async Task<List<Recipe>> GetAllRecipesAsync()
        {
            List <Recipe> recipes= await context.Recipes.ToListAsync();
            return new List<Recipe>(recipes);
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            Recipe recipe= await context.Recipes.FindAsync(id);
            return new Recipe()
            {
                Id=recipe.Id, Name=recipe.Name,Ingredients=recipe.Ingredients,Instruction=recipe.Instruction
            };
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            context.Recipes.Update(recipe);
            await context.SaveChangesAsync();
        }

        public async Task DeleteRecipeAsync(int id)
        {
            var recipe = await context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                context.Recipes.Remove(recipe);
                await context.SaveChangesAsync();
            }
        }

        public async Task<bool> IfUserExistAsync(User user)
        {
            return await context.Users.ContainsAsync(user);
        }

        public async Task<bool> CreateNewUserAsync(User user)
        {
            bool result = await IfUserExistAsync(user);
            if (!result)
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }
            return !result;
        }


    }
}

