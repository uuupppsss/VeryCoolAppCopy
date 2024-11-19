
namespace VeryCoolApp.Model
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Instruction { get; set; }
        public List<IngredientValue> IngredientValues { get; set; }= new List<IngredientValue>();
        public override string ToString()
        {
            return Name;
        }
    }
}
