﻿

namespace VeryCoolApp.Model
{
    public class IngredientValue
    {
        public int Id { get; set; }
        public int RecipeId { get; set; } //Foreign Key
        public int IngredientId { get; set; } //Foreign Key
        public IngredientDTO Ingredient { get; set; }
        public double Quantity { get; set; }

        public override string ToString()
        {  
            return $"{Ingredient.Name} - {Quantity} {Ingredient.Measurement}";
        }
    }
}

