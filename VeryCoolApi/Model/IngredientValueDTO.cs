namespace VeryCoolApi.Model
{
    public class IngredientValueDTO
    {
        public int Id { get; set; }

        public int RecipeId { get; set; }

        public int IngredientId { get; set; }

        public double Quantity { get; set; }
    }
}
