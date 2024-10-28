using VeryCoolApp.Model;

namespace VeryCoolApp.ViewModel
{
    public class AddIngredientPageVM : BaseVM
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

        private string _measurement;

        public string Measurement
        {
            get => _measurement;
            set
            {
                _measurement = value;
                Signal();
            }
        }

        public CommandVM AddNewIngredientCommand { get; set; }

        public AddIngredientPageVM()
        {
            db = new CookingDB();
            AddNewIngredientCommand = new CommandVM(() =>
            {
                if (Name != null && Measurement != null)
                {
                    Ingredient ingredient_to_add = new Ingredient()
                    {
                        Name = Name,
                        Measurement = Measurement
                    };
                    AddNewIngredient(ingredient_to_add);
                }

            });
        }

        private async void AddNewIngredient(Ingredient ingredient)
        {
            await db.AddIngredientAsync(ingredient);
        }
    }
}
