

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

        public async void AddIngredientAsync(Ingredient ingredient)
        {
            await context.AddIngredientAsync(ingredient);
        }

        public async Task<List<Ingredient>> GetAllIngredientsAsync()
        {
            List<Ingredient> ingredients= await context.GetAllIngredientsAsync();
            return await Task.FromResult(new List<Ingredient>(ingredients));
        }

        public async Task<Ingredient> GetIngredientByIdAsync(int id)
        {
            Ingredient ingredient= await context.GetIngredientByIdAsync(id);
            return new Ingredient()
            {
                Id=ingredient.Id,
                Name=ingredient.Name,
                Measurement=ingredient.Measurement,
            };
        }

        public async void UpdateIngredientAsync(Ingredient ingredient)
        {
            await context.UpdateIngredientAsync(ingredient);
        }

        public async void DeleteIngredientAsync(int id)
        {
            await context.DeleteIngredientAsync(id);
        }

        public async void AddRecipeAsync(Recipe recipe)
        {
            await context.AddRecipeAsync(recipe);
        }

        public async Task<List<Recipe>> GetAllRecipesAsync()
        {
            List<Recipe> recipes= await context.GetAllRecipesAsync();
            return new List<Recipe>(recipes);
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            Recipe recipe= await context.GetRecipeByIdAsync(id);
            return new Recipe() { Id=recipe.Id, Name=recipe.Name,Instruction=recipe.Instruction,Ingredients=recipe.Ingredients };
        }

        public async void UpdateRecipeAsync(Recipe recipe)
        {
            await context.UpdateRecipeAsync(recipe);
        }

        public async void DeleteRecipeAsync(int id)
        {
            await context.DeleteRecipeAsync(id);
        }

        public async Task<bool> SignUserUpAsync(User user)
        {
            bool result= await context.IfUserExistAsync(user);
            if (!result)
            {
                await context.CreateNewUserAsync(user);
            }
            return !result;
        }

        public async Task<bool> SignUserInAsync(User user)
        {
            bool result = await context.IfUserExistAsync(user);
            if (result)
            {
                CurrentUser = user;
            }
            return result;
        }

        
    }
}

