

namespace VeryCoolApp.Model
{
    public class IngredientDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Measurement { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Measurement}";
        }
    }
}
