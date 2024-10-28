
namespace VeryCoolApp.Model
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Instruction { get; set; }
        public List<IngredientValueNavigation> Ingredients { get; set; }= new List<IngredientValueNavigation>();
    }
}
