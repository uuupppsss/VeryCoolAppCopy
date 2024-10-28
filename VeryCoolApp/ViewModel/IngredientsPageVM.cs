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
            AddNewIngredient = new CommandVM(() =>
            {
                
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
            await service.AddIngredientAsync(new Ingredient() { Id = 1, Name = "Масло", Measurement = "мл" });
        }

        
    }
}
