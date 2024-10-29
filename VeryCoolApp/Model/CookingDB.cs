
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;


namespace VeryCoolApp.Model
{
    public class CookingDB:DbContext
    {
        private readonly string _filename;

        public  DbSet<Ingredient> Ingredients { get; set; }
        public  DbSet<Recipe> Recipes { get; set; }
        public  DbSet<User> Users { get; set; }
        public  DbSet<IngredientValue> IngredientValues { get; set; }

        public CookingDB(string filename)
        {
            this._filename = filename;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var sqlitePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Database");
            Directory.CreateDirectory(sqlitePath);
            var filename = $"{sqlitePath}\f{_filename}";
            if (!File.Exists(filename))
                File.Create(filename);
            optionsBuilder.UseSqlite($"Data Source={filename}");
            //base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IngredientValue>()
                .HasOne(ingNav => ingNav.Ingredient)
                .WithMany()
                .HasForeignKey(ingNav => ingNav.RecipeId);

            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Ingredients)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
