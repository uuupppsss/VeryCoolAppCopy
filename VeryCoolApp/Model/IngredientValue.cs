

namespace VeryCoolApp.Model
{
    public class IngredientValue
    {
        public int Id { get; set; }
        public Ingredient Ingredient { get; set; }
        public double Quantity { get; set; }

        public override string ToString()
        {
            return $"{Ingredient.Name} - {Quantity} {Ingredient.Measurement}";
        }
    }
}
