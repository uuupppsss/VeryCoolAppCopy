using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeryCoolApp.Model;

namespace VeryCoolApp.ViewModel
{
    public class AddRecipePageVM:BaseVM
    {
		private CookingDB db;
		private string _name;

		public string Name
		{
			get => _name; 
			set 
			{
				_name = value;
				Signal();
			}
		}

		private string _instructions;

		public string Instruction
        {
			get => _instructions; 
			set 
			{
				_instructions = value;
				Signal();
			}
		}

		public ObservableCollection<Ingredient> FullIngredientsList { get; set; }
		private List<IngredientValueNavigation> SelectedIngredientsList;
		public CommandVM AddNewRecipeCommand { get; set; }

        public AddRecipePageVM()
        {
			db=new CookingDB();
			GetIngredients();

			AddNewRecipeCommand = new CommandVM(() =>
			{

			});
		}
		private async void AddNewRecipe(Recipe recipe)
		{
			await db.AddRecipeAsync(recipe);
		}

		private async void GetIngredients()
		{
			FullIngredientsList=await db.GetIngredientsAsync();
		}
    }
}
