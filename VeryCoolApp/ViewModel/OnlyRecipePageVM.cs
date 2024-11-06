using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VeryCoolApp.Model;

namespace VeryCoolApp.ViewModel
{
    [QueryProperty(nameof(SelectedRecipe), "SelectedRecipe")]
    public class OnlyRecipePageVM:BaseVM
    {
        private CookingServise service;

        private Recipe _selectedRecipe;

        public Recipe SelectedRecipe
        
        {
            get => _selectedRecipe; 
            set
            { 
                _selectedRecipe = value;
                Signal();
            }

        }
        public CommandVM DeleteRecipe { get; set; }
        public OnlyRecipePageVM()
        {
            service = CookingServise.Instance;
            //SelectedRecipe=service.SelectedRecipe;

            DeleteRecipe = new CommandVM(async () =>
            {
                await service.DeleteRecipeAsync(SelectedRecipe.Id);
                await Shell.Current.GoToAsync("///RecipesPage");
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
            });
        }
    }
}
