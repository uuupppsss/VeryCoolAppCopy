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
		private int lastInsertRecipeId;
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

		private IngredientDTO _selectedIngredient;

		public IngredientDTO SelectedIngredient
		{
			get =>_selectedIngredient; 
			set 
			{ 
				_selectedIngredient = value;
				Signal();
				SelectedIngredientChanged();
			}
		}


		private List<IngredientDTO> _fullIngredientsList;

		public List<IngredientDTO> FullIngredientsList
        {
			get => _fullIngredientsList; 
			set 
			{
				_fullIngredientsList = value;
				Signal();
			}
		}

		private List<IngredientValue> SelectedIngredientsList;
		public CommandVM AddNewRecipeCommand { get; set; }
		

        public AddRecipePageVM()
        {
			service= CookingServise.Instance;
			dialogServise= DialogServise.Instance;
			SelectedIngredientsList = new List<IngredientValue>();
            GetIngredients();

			AddNewRecipeCommand = new CommandVM(async() =>
			{
				if (Name == null || Instruction == null || SelectedIngredientsList == null)
				{
                    await dialogServise.ShowWarning("Заполните все поля", "Кажется, вы что то пропустили");
                }
				else
				{
					List<IngredientValue> ingredients_values = [..SelectedIngredientsList];
                    await service.AddRecipeAsync(new Recipe()
                    {
                        Name = Name,
                        Instruction = Instruction,
                        IngredientValues = ingredients_values
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
			if(SelectedIngredient!=null)
                await GetInput();
        }
        private async Task GetInput()
        {
            string input = await dialogServise.ShowInputDialog($"Выбранный ингредиент - {SelectedIngredient.Name}", $"Введите колличество, {SelectedIngredient.Measurement}");

            if (!string.IsNullOrEmpty(input)&&double.TryParse(input,out double quality))
            {
				IngredientValue ingredientValue = new IngredientValue()
				{
					IngredientId = SelectedIngredient.Id,
					Quantity = quality,
				};
                


				SelectedIngredientsList.Add(ingredientValue);
                await dialogServise.ShowWarning("Так держать", $"Количество ингредиентов в рецепте: {SelectedIngredientsList.Count}") ;
            }
			else
			{
                await dialogServise.ShowWarning("Что то не так с данными", "Введите число");
                return;
			}
        }

    }
}
