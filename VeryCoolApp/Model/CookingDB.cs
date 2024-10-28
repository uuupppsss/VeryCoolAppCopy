
using Microsoft.EntityFrameworkCore;


namespace VeryCoolApp.Model
{
    public class CookingDB:DbContext
    {
        private readonly string filename;

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<IngredientValueNavigation> IngredientValueNavigations { get; set; }

        public CookingDB(string filename)
        {
            this.filename = filename;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={filename}");
            base.OnConfiguring(optionsBuilder);
        }

        // Методы для работы с Ingredient

        public async Task AddIngredientAsync(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
            await SaveChangesAsync();
        }

        public async Task<List<Ingredient>> GetAllIngredientsAsync()
        {
            return await Ingredients.ToListAsync();
        }

        public async Task<Ingredient> GetIngredientByIdAsync(int id)
        {
            return await Ingredients.FindAsync(id);
        }

        public async Task UpdateIngredientAsync(Ingredient ingredient)
        {
            Ingredients.Update(ingredient);
            await SaveChangesAsync();
        }

        public async Task DeleteIngredientAsync(int id)
        {
            var ingredient = await Ingredients.FindAsync(id);
            if (ingredient != null)
            {
                Ingredients.Remove(ingredient);
                await SaveChangesAsync();
            }
        }

        // Методы для работы с Recipe
        public async Task AddRecipeAsync(Recipe recipe)
        {
            Recipes.Add(recipe);
            await SaveChangesAsync();
        }

        public async Task<List<Recipe>> GetAllRecipesAsync()
        {
            return await Recipes.ToListAsync();
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            return await Recipes.FindAsync(id);
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            Recipes.Update(recipe);
            await SaveChangesAsync();
        }

        public async Task DeleteRecipeAsync(int id)
        {
            var recipe = await Recipes.FindAsync(id);
            if (recipe != null)
            {
                Recipes.Remove(recipe);
                await SaveChangesAsync();
            }
        }

        public async Task<bool> IfUserExistAsync(User user)
        {
            return await Users.ContainsAsync(user);
        }

        public async Task<bool> CreateNewUserAsync(User user)
        {
            bool result= await IfUserExistAsync(user);
            if (!result)
            {
                Users.Add(user);
                await SaveChangesAsync();
            }
            return !result;
        }
    }
}
