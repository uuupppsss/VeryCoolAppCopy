﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeryCoolApp.Model;

namespace VeryCoolApp.ViewModel
{
    public class RecipesPageVM:BaseVM
    {
        private CookingServise service;
        DialogServise dialogServise;

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

        private Recipe _selectedRecipe;

        public Recipe SelectedRecipe
        {
            get => _selectedRecipe;
            set 
            { 
                _selectedRecipe = value;
                SelectedRecipeChanged(value);
                Signal();
            }
        }

        public CommandVM AddNewRecipe {  get; set; }

        public RecipesPageVM()
        {
            service = CookingServise.Instance;
            dialogServise = DialogServise.Instance;
            service.RecipesCollectionChanged += GetRecipesAsync;
            GetRecipesAsync();
            AddNewRecipe = new CommandVM(async() =>
            {
                await Shell.Current.GoToAsync("AddRecipePage");
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
            });
            SelectedRecipe = null;
        }

        private async void GetRecipesAsync()
        {
            Recipes = await service.GetAllRecipesAsync();
            
        }
        
        private async void SelectedRecipeChanged(Recipe recipe)
        {
            if (recipe != null)
            {
                //service.SelectedRecipe = recipe;
                var navigationParameter = new Dictionary<string, object>
                {
                    { "SelectedRecipe", recipe }
                };
                await Shell.Current.GoToAsync("OnlyRecipe", navigationParameter);
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
            }
        }
    }
}
