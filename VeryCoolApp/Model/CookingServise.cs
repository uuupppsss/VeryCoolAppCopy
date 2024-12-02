
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

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

        public async Task CreateNewIngredientValues(List<IngredientValue> ingredientValues, int recipe_id)
        {
            List<IngredientValueDTO> content= new List<IngredientValueDTO>();
            foreach (var iv in ingredientValues)
            {
                content.Add(new IngredientValueDTO()
                {
                    RecipeId = recipe_id,
                    Quantity = iv.Quantity,
                    IngredientId = iv.IngredientId
                });
            }
            try
            {
                string json = JsonSerializer.Serialize(content);
                var responce = await client.PostAsync($"Ingredients/CreateNewIngredientValues",
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

        public async Task<List<IngredientValue>> GetIngredientValuesByRecipeId(int recipe_id)
        {
            try
            {
                var responce = await client.GetAsync($"Ingredients/GetIngredientValuesByRecipeId?recipe_id={recipe_id}");
                if (!responce.IsSuccessStatusCode)
                {
                    await dialogServise.ShowWarning("Что то пошло не так ", $"Ошибка: {responce.StatusCode}");
                    return null;
                }
                else
                {
                    var ingredient_values = await responce.Content.ReadFromJsonAsync<List<IngredientValueDTO>>();
                    List<IngredientDTO> ingredients = await GetAllIngredientsAsync();
                    var result = new List<IngredientValue>();
                    foreach (var iv in ingredient_values)
                    {
                        result.Add(new IngredientValue()
                        {
                            Id= iv.Id,
                            IngredientId= iv.IngredientId,
                            RecipeId= iv.RecipeId,
                            Quantity= iv.Quantity,
                            Ingredient=ingredients.FirstOrDefault(i=>i.Id==iv.IngredientId)
                        });
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                await dialogServise.ShowWarning("Всё сломалось ", ex.ToString());
                return null;
            }
        }

        // Методы для работы с Recipe
        public async Task<int> AddRecipeAsync(Recipe recipe)
        {
            RecipeDTO result = new RecipeDTO()
            {
                Name = recipe.Name,
                Instruction = recipe.Instruction,
            };
            try
            {
                string json = JsonSerializer.Serialize(result);
                var responce = await client.PostAsync($"Recipes/CreateNewRecipe",
                    new StringContent(json, Encoding.UTF8, "application/json"));
                if (!responce.IsSuccessStatusCode)
                {
                    await dialogServise.ShowWarning("Что то пошло не так ", $"Ошибка: {responce.StatusCode}");
                    return 0;
                }
                else
                {
                    return await responce.Content.ReadFromJsonAsync<int>();
                }
            }
            catch (Exception ex)
            {
                await dialogServise.ShowWarning("Всё сломалось ", ex.ToString());
                return 0;
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
                    var recipes = await responce.Content.ReadFromJsonAsync<List<RecipeDTO>>();
                    List<IngredientDTO> ingredients = await GetAllIngredientsAsync();
                    var result = new List<Recipe>();
                    foreach (var r in recipes)
                    {
                        result.Add(new Recipe()
                        {
                            Id = r.Id,
                            Name = r.Name,
                            Instruction = r.Instruction,
                            IngredientValues= await GetIngredientValuesByRecipeId(r.Id),
                        });
                    }
                    return result;
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
                    var result= await responce.Content.ReadFromJsonAsync<RecipeDTO>();
                    return new Recipe()
                    {

                    };
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

        public async Task DefineIngredientValuesForRecipe(int recipe_id)
        {
            try
            {
                var responce = await client.GetAsync($"Recipes/DefineIngredientValuesForRecipe?recipe_id={recipe_id}");
                if (!responce.IsSuccessStatusCode)
                {
                    await dialogServise.ShowWarning("Что то пошло не так ", $"Ошибка: {responce.StatusCode}");
                }
                else
                {
                    await dialogServise.ShowWarning("Успех ", $"Рецепт успешно добавлен!");
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
                    await dialogServise.ShowWarning("Успех ", $"Пользователь {user.Login} зарегистрирован успешно ");
                }
            }
            catch (Exception ex)
            {
                await dialogServise.ShowWarning("Всё сломалось ", ex.ToString());
            }
        }

    }
}

