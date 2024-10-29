
using Microsoft.EntityFrameworkCore;


namespace VeryCoolApp.Model
{
    public class CookingDB:DbContext
    {
        private readonly string _filename;

        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<IngredientValueNavigation> IngredientValueNavigations { get; set; }

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

        
    }
}
