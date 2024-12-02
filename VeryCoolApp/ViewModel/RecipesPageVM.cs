using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeryCoolApp.Model;
using VeryCoolApp.Pages;

namespace VeryCoolApp.ViewModel
{
    public class RecipesPageVM:BaseVM
    {
        private CookingServise service;
        //DialogServise dialogServise;

        private List<Recipe> _recipes;

        public List<Recipe> Recipes
        {
            get => _recipes;
            set 
            {
                _recipes = value;
                Signal();
            }
        }

        //private Recipe _selectedRecipe;

        //public Recipe SelectedRecipe
        //{
        //    get => _selectedRecipe;
        //    set 
        //    { 
        //        _selectedRecipe = value;
        //        SelectedRecipeChanged(value);
        //        Signal();
        //    }
        //}

        public CommandVM AddNewRecipe {  get; set; }

        //public CommandVM RecipeTapped { get; set; }
        

        public RecipesPageVM()
        {
            service = CookingServise.Instance;
            //dialogServise = DialogServise.Instance;
            service.RecipesCollectionChanged += GetRecipesAsync;
            GetRecipesAsync();
            AddNewRecipe = new CommandVM(async() =>
            {
                await Shell.Current.GoToAsync("AddRecipePage");
            });
            //RecipeTapped = new CommandVM(async()=>
            //{
            //    if (SelectedRecipe != null)
            //    {
            //        var navigationParameter = new ShellNavigationQueryParameters
            //        {
            //            { "SelectedRecipe", SelectedRecipe }
            //        };
            //        await Shell.Current.GoToAsync("OnlyRecipe", navigationParameter);
            //    }
            //});
            //SelectedRecipe = null;
        }

        private async void GetRecipesAsync()
        {
            Recipes = await service.GetAllRecipesAsync();
            
        }

        public async void SelectedRecipeChanged(Recipe recipe)
        {
            if (recipe != null)
            {
                var navigationParameter = new ShellNavigationQueryParameters
                {
                    { "SelectedRecipe", recipe }
                };
                await Shell.Current.GoToAsync("OnlyRecipe", navigationParameter);
            }
        }
    }
}
