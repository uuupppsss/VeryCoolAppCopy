
using System.Text;
using System.Text.Json;
using System.Net.Http.Json;

namespace VeryCoolApp.Model
{
    public class CookingServise
    {
        private HttpClient client;
        private DialogServise dialogServise;
        static CookingServise instance;
        public event Action IngredientsCollectionChanged;
        public event Action RecipesCollectionChanged;


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
                BaseAddress = new Uri("http://10.0.2.2:5161/api/")
            };
            dialogServise=DialogServise.Instance;
        }

        // Методы для работы с Ingredient

        public async Task AddIngredientAsync(IngredientDTO ingredient)
        {
            try
            {
                string json = JsonSerializer.Serialize(ingredient);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var responce = await client.PostAsync("Ingredients/CreateNewIngredient", content);
                if (!responce.IsSuccessStatusCode)
                {
                    await dialogServise.ShowWarning("Что то пошло не так ", $"Ошибка: {responce.StatusCode}");
                }
                else await dialogServise.ShowWarning("Успех ", $"Ингредиент {ingredient.Name} успешно добавлен!");
                IngredientsCollectionChanged.Invoke();
            }
            catch (Exception ex)
            {
                await dialogServise.ShowWarning("Всё сломалось ", ex.ToString());
            }
        }

        public async Task<List<IngredientDTO>> GetAllIngredientsAsync()
        {
            try
            {
                var responce = await client.GetAsync("Ingredients/GetIngredientsList");
                if (!responce.IsSuccessStatusCode)
                {
                    await dialogServise.ShowWarning("Что то пошло не так ", $"Ошибка: {responce.StatusCode}");
                    return null;
                }
                else return await responce.Content.ReadFromJsonAsync<List<IngredientDTO>>();
            }
            catch (Exception ex)
            {
                await dialogServise.ShowWarning("Всё сломалось ", ex.ToString());
                return null;
            }
        }

        public async Task<IngredientDTO> GetIngredientByIdAsync(int id)
        {
            try
            {
                var responce = await client.GetAsync($"Ingredients/GetIngredientById?id={id}");
                if (!responce.IsSuccessStatusCode)
                {
                    await dialogServise.ShowWarning("Что то пошло не так ", $"Ошибка: {responce.StatusCode}");
                    return null;
                }
                else
                {
                    return await responce.Content.ReadFromJsonAsync<IngredientDTO>();
                }
            }
            catch (Exception ex)
            {
                await dialogServise.ShowWarning("Всё сломалось ", ex.ToString());
                return null;
            }
        }


        public async Task DeleteIngredientAsync(int id)
        {
            try
            {
                var responce = await client.GetAsync($"Ingredients/DeleteIngredient?id={id}");
                if (!responce.IsSuccessStatusCode)
                {
                    await dialogServise.ShowWarning("Что то пошло не так ", $"Ошибка: {responce.StatusCode}");
                }
                else
                {
                    await dialogServise.ShowWarning("Успех ", "Ингредиент успешно удалён!");
                    IngredientsCollectionChanged.Invoke();
                }
            }
            catch (Exception ex)
            {
                await dialogServise.ShowWarning("Всё сломалось ", ex.ToString());
            }
        }

        // Методы для работы с Recipe
        public async Task AddRecipeAsync(Recipe recipe)
        {
            try
            {
                string json = JsonSerializer.Serialize(recipe);
                var responce = await client.PostAsync($"Recipes/CreateNewRecipe",
                    new StringContent(json, Encoding.UTF8, "application/json"));
                if (!responce.IsSuccessStatusCode)
                {
                    await dialogServise.ShowWarning("Что то пошло не так ", $"Ошибка: {responce.StatusCode}");
                }
                else
                {
                    await dialogServise.ShowWarning("Успех ", "Рецепт успешно добавлен!");
                    RecipesCollectionChanged.Invoke();
                }
            }
            catch (Exception ex)
            {
                await dialogServise.ShowWarning("Всё сломалось ", ex.ToString());
            }

        }

        public async Task<List<Recipe>> GetAllRecipesAsync()
        {
            try
            {
                var responce = await client.GetAsync($"Recipes/GetRecipesList");
                if (!responce.IsSuccessStatusCode)
                {
                    await dialogServise.ShowWarning("Что то пошло не так ", $"Ошибка: {responce.StatusCode}");
                    return null;
                }
                else
                {
                    return await responce.Content.ReadFromJsonAsync<List<Recipe>>();
                }
            }
            catch (Exception ex)
            {
                await dialogServise.ShowWarning("Всё сломалось ", ex.ToString());
                return null;
            }

        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            try
            {
                var responce = await client.GetAsync($"Recipes/GetRecipeById?id={id}");
                if (!responce.IsSuccessStatusCode)
                {
                    await dialogServise.ShowWarning("Что то пошло не так ", $"Ошибка: {responce.StatusCode}");
                    return null;
                }
                else
                {
                    return await responce.Content.ReadFromJsonAsync<Recipe>();
                }
            }
            catch (Exception ex)
            {
                await dialogServise.ShowWarning("Всё сломалось ", ex.ToString());
                return null;
            }
        }


        public async Task DeleteRecipeAsync(int id)
        {
            try
            {
                var responce = await client.GetAsync($"Recipes/DeleteRecipe?id={id}");
                if (!responce.IsSuccessStatusCode)
                {
                    await dialogServise.ShowWarning("Что то пошло не так ", $"Ошибка: {responce.StatusCode}");
                }
                else
                {
                    await dialogServise.ShowWarning("Успех ", "Рецепт успешно удален!");
                    RecipesCollectionChanged.Invoke();
                }
            }
            catch (Exception ex)
            {
                await dialogServise.ShowWarning("Всё сломалось ", ex.ToString());
            }
        }

        //Работа с User
        public async Task<bool> IfUserExistAsync(string login)
        {
            try
            {
                var responce = await client.GetAsync($"Recipes/DeleteRecipe?login={login}");
                if (!responce.IsSuccessStatusCode)
                {
                    await dialogServise.ShowWarning("Что то пошло не так ", $"Ошибка: {responce.StatusCode}");
                    return false;
                }
                else
                {
                    return await responce.Content.ReadFromJsonAsync<bool>();
                }
            }
            catch (Exception ex)
            {
                await dialogServise.ShowWarning("Всё сломалось ", ex.ToString());
                return false;
            }
        }

        public async Task<bool> SignUserInAsync(User user)
        {
            try
            {
                string json = JsonSerializer.Serialize(user);
                var responce = await client.PostAsync($"User/SignUserIn",
                    new StringContent(json, Encoding.UTF8, "application/json"));
                if (!responce.IsSuccessStatusCode)
                {
                    await dialogServise.ShowWarning("Что то пошло не так ", $"Ошибка: {responce.StatusCode}");
                    return false;
                }
                else
                {
                    return await responce.Content.ReadFromJsonAsync<bool>();
                }
            }
            catch (Exception ex)
            {
                await dialogServise.ShowWarning("Всё сломалось ", ex.ToString());
                return false;
            }

        }

        public async Task CreateNewUserAsync(User user)
        {
            try
            {
                string json = JsonSerializer.Serialize(user);
                var responce = await client.PostAsync($"User/CreateNewUser",
                    new StringContent(json, Encoding.UTF8, "application/json"));
                if (!responce.IsSuccessStatusCode)
                {
                    await dialogServise.ShowWarning("Что то пошло не так ", $"Ошибка: {responce.StatusCode}");
                }
                else
                {
                    await dialogServise.ShowWarning("Успех ", $"Пользователь {user.Login} зарегистрирован успеiно ");
                }
            }
            catch (Exception ex)
            {
                await dialogServise.ShowWarning("Всё сломалось ", ex.ToString());
            }
        }

    }
}

