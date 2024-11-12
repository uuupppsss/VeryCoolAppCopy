using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeryCoolApi.Model;

namespace VeryCoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        readonly VeryCoolDbContext context;
        public IngredientsController(VeryCoolDbContext context)
        {
            this.context = context;
        }

        [HttpPost("CreateNewIngredient")]
        public async Task<ActionResult> CreateNewIngredient(Ingredient ingredient)
        {
            if (ingredient == null) return BadRequest("Invalid data");
            context.Ingredients.Add(ingredient);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("GetIngredientsList")]
        public async Task<ActionResult<List<Ingredient>>> GetIngredientsList()
        {
            List<Ingredient> result = [.. context.Ingredients];
            return Ok(result);
        }

        [HttpGet("GetIngredientById")]
        public async Task<ActionResult<Ingredient>> GetIngredientById(int id)
        {
            if (id==0) return BadRequest("Invalid data");
            Ingredient ingredient= await context.Ingredients.FirstOrDefaultAsync(x => x.Id==id);
            if (ingredient == null) return NotFound();
            return Ok(ingredient);
        }

        [HttpGet("DeleteIngredient")]
        public async Task<ActionResult> DeleteIngredient(int id)
        {
            if (id == 0) return BadRequest("Invalid data");
            Ingredient ingredient = await context.Ingredients.FirstOrDefaultAsync(x => x.Id == id);
            if (ingredient == null) return NotFound();
            context.Ingredients.Remove(ingredient);
            await context.SaveChangesAsync();
            return  Ok();
        }
    }
}
