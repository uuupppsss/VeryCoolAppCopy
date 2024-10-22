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
        private CookingDB _cookingDB;

        private ObservableCollection<Ingredient> _ingredients;

        public ObservableCollection<Ingredient> Ingredients
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
            _cookingDB = new CookingDB();
            GetIngredientsAsync();

            Ingredients.Add(new Ingredient() { Name = "шмели", Measurement = "шт" });
            Ingredients.Add(new Ingredient() { Name = "масло", Measurement = "мл" });
            Ingredients.Add(new Ingredient() { Name = "гвозди", Measurement = "г" });
            Ingredients.Add(new Ingredient() { Name = "кляр", Measurement = "г" });

            AddNewIngredient = new CommandVM(() =>
            {

            });

            RemoveIngredient = new CommandVM(() =>
            {

            });
        }

        private async void GetIngredientsAsync()
        {
            Ingredients = await _cookingDB.GetIngredientsAsync();
        }
    }
}
