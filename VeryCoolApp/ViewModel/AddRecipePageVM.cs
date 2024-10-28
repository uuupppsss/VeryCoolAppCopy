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
		private CookingServise service;
		private DialogServise dialogServise;
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

		private Ingredient _selectedIngredient;

		public Ingredient SelectedIngredient
		{
			get =>_selectedIngredient; 
			set 
			{ 
				_selectedIngredient = value;
				Signal();
				SelectedIngredientChanged();
			}
		}


		public List<Ingredient> FullIngredientsList { get; set; }
		private List<IngredientValueNavigation> SelectedIngredientsList;
		public CommandVM AddNewRecipeCommand { get; set; }
		

        public AddRecipePageVM()
        {
			service= CookingServise.Instance;
			dialogServise= DialogServise.Instance;
            GetIngredients();

			AddNewRecipeCommand = new CommandVM(async() =>
			{
				if (Name == null || Instruction == null || SelectedIngredientsList == null)
				{
                    await dialogServise.ShowWarning("Заполните все поля", "Кажется, вы что то пропустили");
                }
				else
				{
                    await service.AddRecipeAsync(new Recipe()
                    {
                        Name = Name,
                        Instruction = Instruction,
                        Ingredients = SelectedIngredientsList
                    });
                }
				
			});
            
        }


		private async void GetIngredients()
		{
			FullIngredientsList=await service.GetAllIngredientsAsync();
		}

        private async void SelectedIngredientChanged()
        {
			await GetInput();
        }
        private async Task GetInput()
        {
            string input = await dialogServise.ShowInputDialog("Введите данные", "Введите колличество");

            if (!string.IsNullOrEmpty(input)&&double.TryParse(input,out double quality))
            {
				SelectedIngredientsList.Add(new IngredientValueNavigation()
				{
                    Ingredient=SelectedIngredient,
					Quantity=quality
                });
            }
			else
			{
                await dialogServise.ShowWarning("Что то не так с данными", "Введите число");
                return;
			}
        }

    }
}
