using VeryCoolApp.Model;

namespace VeryCoolApp.ViewModel
{
    public class AddIngredientPageVM : BaseVM
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
            service = CookingServise.Instance;
            dialogServise = DialogServise.Instance;
            AddNewIngredientCommand = new CommandVM(async() =>
            {
                if (Name != null && Measurement != null)
                {
                    IngredientDTO ingredient_to_add = new IngredientDTO()
                    {
                        Name = Name,
                        Measurement = Measurement
                    };
                    await service.AddIngredientAsync(ingredient_to_add);
                }
                else
                {
                    await dialogServise.ShowWarning("Заполните все поля", "Кажется, вы что то пропустили");
                }

            });
        }

    }
}
