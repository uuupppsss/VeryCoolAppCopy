using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
