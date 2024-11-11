using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> CreateNewRecipe(Recipe recipe)
        {
            if (recipe == null) return BadRequest("Invalid data");
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

        }
    }
}
