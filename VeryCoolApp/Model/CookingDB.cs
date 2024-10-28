
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

        
    }
}
