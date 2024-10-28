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
            AddIngredient(new Ingredient() { Id = 1, Name = "Масло", Measurement = "мл" });
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
            Ingredients = await _cookingDB.GetIngredientsAsync();
        }

        private async void AddIngredient(Ingredient ingredient)
        {
            await _cookingDB.AddIngredientAsync(ingredient);
        }
        
    }
}
