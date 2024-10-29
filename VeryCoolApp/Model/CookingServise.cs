
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;



namespace VeryCoolApp.Model
{
    public class CookingServise
    {
        //public User CurrentUser { get; set; }
        public Recipe SelectedRecipe { get; set; }
        //public int LastInsertRecipeId { get; set; }

        public event Action IngredientsCollectionChanged;
        public event Action RecipesCollectionChanged;
        //public event Action UsersCollectionChanged;
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
            context = new CookingDB("CookingServiseDB");
            context.Database.EnsureCreated();
        }

        // Методы для работы с Ingredient

        public async Task AddIngredientAsync(Ingredient ingredient)
        {
            context.Ingredients.Add(ingredient);
            await context.SaveChangesAsync();
            IngredientsCollectionChanged.Invoke();
        }

        public async Task<List<Ingredient>> GetAllIngredientsAsync()
        {
            List<Ingredient> ingredients= await context.Ingredients.ToListAsync();
            return new List<Ingredient>(ingredients);
        }

        public async Task<Ingredient> GetIngredientByIdAsync(int id)
        {
            Ingredient ingredient = await context.Ingredients.FirstOrDefaultAsync(r => r.Id == id);
            return new Ingredient()
            {
                Id=ingredient.Id, Name=ingredient.Name,Measurement=ingredient.Measurement
            };
        }

        public async Task UpdateIngredientAsync(Ingredient ingredient)
        {
            context.Ingredients.Update(ingredient);
            await context.SaveChangesAsync();
            IngredientsCollectionChanged.Invoke();
        }

        public async Task DeleteIngredientAsync(int id)
        {
            var ingredient = await context.Ingredients.FirstOrDefaultAsync(r => r.Id == id);
            if (ingredient != null)
            {
                context.Ingredients.Remove(ingredient);
                await context.SaveChangesAsync();
                IngredientsCollectionChanged.Invoke();
            }
        }

        // Методы для работы с Recipe
        public async Task AddRecipeAsync(Recipe recipe)
        {
            context.Recipes.Add(recipe);
            await context.SaveChangesAsync();
            RecipesCollectionChanged.Invoke();
        }

        public async Task<List<Recipe>> GetAllRecipesAsync()
        {
            List <Recipe> recipes= await context.Recipes.ToListAsync();
            return new List<Recipe>(recipes);
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            Recipe recipe= await context.Recipes.FirstOrDefaultAsync(r => r.Id == id);
            return new Recipe()
            {
                Id=recipe.Id, Name=recipe.Name,Ingredients=recipe.Ingredients,Instruction=recipe.Instruction
            };
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            context.Recipes.Update(recipe);
            await context.SaveChangesAsync();
            RecipesCollectionChanged.Invoke();
        }

        public async Task DeleteRecipeAsync(int id)
        {
            var recipe = await context.Recipes.FirstOrDefaultAsync(r => r.Id == id);
            if (recipe != null)
            {
                context.Recipes.Remove(recipe);
                await context.SaveChangesAsync();
                RecipesCollectionChanged.Invoke();
            }
        }

        public async Task<bool> IfUserExistAsync(User user)
        {
            return await context.Users.ContainsAsync(user);
        }

        public async Task CreateNewUserAsync(User user)
        {
            bool result = await IfUserExistAsync(user);
            if (!result||user!=null)
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }
        }

        public int GetLastInsertRecipeId()
        {
            int id = context.Recipes.Max(i => i.Id);
            return id;
        }
        public async Task CreateNewIngredientValueEssence(IngredientValue essence)
        {
            if (essence != null)
            {
                context.IngredientValues.Add(essence);
                await context.SaveChangesAsync();
            }
        }
        public async Task<IngredientValue> GetLastInsertIngredientValueEssence()
        {
            int id = context.IngredientValues.Max(i => i.Id);
            return await context.IngredientValues.FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}

