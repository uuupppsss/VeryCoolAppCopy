
using System.Text;
using System.Text.Json;
using System.Net.Http.Json;

namespace VeryCoolApp.Model
{
    public class CookingServise
    {
        private HttpClient client;
        static CookingServise instance;

        public static CookingServise Instance
        {
            get
            {
                if (instance == null)
                    instance = new CookingServise();
                return instance;
            }
        }

        public CookingServise()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("http://10.0.2.2:5161/api")
            };
        }

        // Методы для работы с Ingredient

        public async Task AddIngredientAsync(Ingredient ingredient)
        {
            string arg=JsonSerializer.Serialize(ingredient);
            var responce= await client.PostAsync($"Ingredients/CreateNewIngredient?ingredient={arg}",
              new StringContent(arg, Encoding.UTF8, "application/json"));
            responce.EnsureSuccessStatusCode();
        }

        public async Task<List<Ingredient>> GetAllIngredientsAsync()
        {
            var responce = await client.GetAsync("Ingredients/GetIngredientsList");
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await responce.Content.ReadFromJsonAsync<List<Ingredient>>();            }
            else
            {
                return null;
            }
        }

        public async Task<Ingredient> GetIngredientByIdAsync(int id)
        {
            var responce = await client.GetAsync($"Ingredients/GetIngredientById?id={id}");
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await responce.Content.ReadFromJsonAsync<Ingredient>();
            }
            else
            {
                return null;
            }
        }


        public async Task DeleteIngredientAsync(int id)
        {

        }

        // Методы для работы с Recipe
        public async Task AddRecipeAsync(Recipe recipe)
        {
            string arg=JsonSerializer.Serialize(recipe);
            var responce = await client.PostAsync($"Recipes/CreateNewRecipe?recipe={arg}",
                new StringContent(arg, Encoding.UTF8, "application/json"));
            responce.EnsureSuccessStatusCode();
        }

        public async Task<List<Recipe>> GetAllRecipesAsync()
        {
            var responce = await client.GetAsync("Recipes/GetRecipesList");
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await responce.Content.ReadFromJsonAsync<List<Recipe>>();
            }
            else
            {
                return null;
            }
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            var responce = await client.GetAsync($"Recipes/GetRecipeById?id={id}");
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await responce.Content.ReadFromJsonAsync<Recipe>();
            }
            else
            {
                return null;
            }
        }


        public async Task DeleteRecipeAsync(int id)
        {

        }

        public async Task<bool> IfUserExistAsync(string login)
        {

        }

        public async Task<bool> SignUserInAsync(User user)
        {
            string arg = JsonSerializer.Serialize(user);
            var responce = client.GetAsync($"User/SignUserIn?user={arg}");
        }

        public async Task CreateNewUserAsync(User user)
        {

        }

    }
}

