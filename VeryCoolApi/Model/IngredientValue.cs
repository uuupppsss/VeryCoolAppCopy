using System;
using System.Collections.Generic;

namespace VeryCoolApi.Model;

public partial class IngredientValue
{
    public int Id { get; set; }

    public int RecipeId { get; set; }

    public int IngredientId { get; set; }

    public double Quantity { get; set; }

    public virtual Ingredient Ingredient { get; set; } = null!;

    public virtual Recipe Recipe { get; set; } = null!;
}
