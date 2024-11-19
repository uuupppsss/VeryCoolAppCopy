using System;
using System.Collections.Generic;

namespace VeryCoolApi.Model;

public partial class Recipe
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Instruction { get; set; } = null!;

    public virtual ICollection<IngredientValue> IngredientValues { get; set; } = [];
}
