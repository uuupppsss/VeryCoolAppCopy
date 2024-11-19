using System;
using System.Collections.Generic;

namespace VeryCoolApi.Model;

public partial class Ingredient
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Measurement { get; set; } = null!;

    public virtual ICollection<IngredientValue> IngredientValues { get; set; } = [];
}
