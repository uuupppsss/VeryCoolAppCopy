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

        private List<Ingredient> _ingredients;

        public List<Ingredient> Ingredients
        {
            get => _ingredients;
            set { _ingredients = value; }
        }

        private Ingredient _selectedIngredient;

        public Ingredient SelectedIngredient
        {
            get => _selectedIngredient;
            set { _selectedIngredient = value; }
        }

        public CommandVM AddNewIngredient {  get; set; }
        public CommandVM RemoveIngredient { get; set; }

        public IngredientsPageVM()
        {
            service = CookingServise.Instance;
            CreateDemoIngredients();
            GetIngredients();
            AddNewIngredient = new CommandVM(async() =>
            {
                await Shell.Current.GoToAsync("AddIngridientPage");
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
            });

            RemoveIngredient = new CommandVM(() =>
            {

            });
        }

        private async void GetIngredients()
        {
            Ingredients = await service.GetAllIngredientsAsync();
        }

        private async void CreateDemoIngredients()
        {
            await service.AddIngredientAsync(new Ingredient() { Name = "Масло", Measurement = "мл" });
        }

        
    }
}
