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
        public async Task<ActionResult> CreateNewRecipe(RecipeDTO recipedto)
        {
            if (recipedto == null) return BadRequest("Invalid data");
            List<IngredientValue> ingredientValues = [];
            //oao mmmmmm aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa pmgt
            Recipe recipe = new Recipe()
            {
                Name = recipedto.Name,
                Instruction = recipedto.Instruction,
                IngredientValues= ingredientValues
            };
            context.Recipes.Add(recipe);
            await context.SaveChangesAsync();
            return Ok();
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
            Recipe recipe= await context.Recipes.FirstOrDefaultAsync(r=>r.Id==id);
            if (recipe == null) return NotFound();
            return Ok(recipe);
        }

        [HttpGet("DeleteRecipe")]
        public async Task<ActionResult> DeleteRecipe(int id)
        {
            if(id==0) return BadRequest("Invalid data");
            Recipe recipe = await context.Recipes.FirstOrDefaultAsync(r=> r.Id==id);
            if (recipe == null) return NotFound();
            context.Recipes.Remove(recipe);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
