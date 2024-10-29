using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeryCoolApp.Model;

namespace VeryCoolApp.ViewModel
{
    public class IngredientsPageVM:BaseVM
    {
        private CookingServise service;
        private DialogServise dialogServise;

        private List<Ingredient> _ingredients;

        public List<Ingredient> Ingredients
        {
            get => _ingredients;
            set 
            { 
                _ingredients = value;
                Signal();
            }
        }

        private Ingredient _selectedIngredient;

        public Ingredient SelectedIngredient
        {
            get => _selectedIngredient;
            set {
                _selectedIngredient = value;
                Signal();
            }
        }

        public CommandVM AddNewIngredient {  get; set; }
        public CommandVM RemoveIngredient { get; set; }

        public IngredientsPageVM()
        {
            service = CookingServise.Instance;
            dialogServise = DialogServise.Instance;
            service.IngredientsCollectionChanged += GetIngredients;
            GetIngredients();
            AddNewIngredient = new CommandVM(async() =>
            {
                await Shell.Current.GoToAsync("AddIngridientPage");
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
            });

            RemoveIngredient = new CommandVM(async () =>
            {
                if (SelectedIngredient == null)
                {
                    await dialogServise.ShowWarning("Себя удали", "Выберите объект для удаления");
                }
                else
                {
                    await service.DeleteIngredientAsync(SelectedIngredient.Id);
                }
            })
            {

            };
        }

        private async void GetIngredients()
        {
            Ingredients = await service.GetAllIngredientsAsync();
        }


    }
}
