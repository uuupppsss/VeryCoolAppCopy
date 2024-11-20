using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeryCoolApi.Model;

namespace VeryCoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        readonly VeryCoolDbContext context;
        public RecipesController(VeryCoolDbContext context)
        {
            this.context = context;
        }

        [HttpPost("CreateNewRecipe")]
        public async Task<ActionResult<int>> CreateNewRecipe(RecipeDTO recipedto)
        {
            if (recipedto == null) return BadRequest("Invalid data");
            List<IngredientValue> ingredientValues = [];
            Recipe recipe = new Recipe()
            {
                Name = recipedto.Name,
                Instruction = recipedto.Instruction,
                IngredientValues = ingredientValues
            };
            context.Recipes.Add(recipe);
            await context.SaveChangesAsync();
            Recipe just_added_recipe=context.Recipes.ToList().Last();
            return Ok(just_added_recipe.Id);
        }

        [HttpGet("GetRecipesList")]
        public async Task<ActionResult<List<Recipe>>> GetRecipesList()
        {
            List<Recipe> result = [.. context.Recipes];
            return Ok(result);
        }

        [HttpGet("GetRecipeById")]
        public async Task<ActionResult<Recipe>> GetRecipeById(int id)
        {
            if (id == 0) return BadRequest("Invalid data");
            Recipe recipe = await context.Recipes.FirstOrDefaultAsync(r => r.Id == id);
            if (recipe == null) return NotFound();
            return Ok(recipe);
        }

        [HttpGet("DeleteRecipe")]
        public async Task<ActionResult> DeleteRecipe(int id)
        {
            if (id == 0) return BadRequest("Invalid data");
            Recipe recipe = await context.Recipes.FirstOrDefaultAsync(r => r.Id == id);
            if (recipe == null) return NotFound();
            context.Recipes.Remove(recipe);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("DefineIngredientValuesForRecipe")]
        public async Task<ActionResult> DefineIngredientValuesForRecipe(int recipe_id)
        {
            if(recipe_id == 0) return BadRequest("Invalid data");
            List<IngredientValue> ingredientValues = new List<IngredientValue>();
            ingredientValues.AddRange(context.IngredientValues.Where(iv => iv.RecipeId == recipe_id));
            Recipe recipe = context.Recipes.FirstOrDefault(r => r.Id == recipe_id);
            if (recipe == null) return NotFound();
            recipe.IngredientValues = ingredientValues;
            context.Recipes.Update(recipe);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
