﻿using VeryCoolApp.Model;

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
                    Ingredient ingredient_to_add = new Ingredient()
                    {
                        Name = Name,
                        Measurement = Measurement
                    };
                    await service.AddIngredientAsync(ingredient_to_add);
                    await dialogServise.ShowWarning("Всё чики пуки", "Ингридиент добавлен");
                }
                else
                {
                    await dialogServise.ShowWarning("Заполните все поля", "Кажется, вы что то пропустили");
                }

            });
        }

    }
}
