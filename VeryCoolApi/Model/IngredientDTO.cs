﻿using System;
using System.Collections.Generic;

namespace VeryCoolApi.Model;

public partial class IngredientDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Measurement { get; set; } = null!;
}
