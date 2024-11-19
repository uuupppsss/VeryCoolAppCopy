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
        public async Task<ActionResult> CreateNewIngredient(IngredientDTO ingredientdto)
        {
            if (ingredientdto == null) return BadRequest("Invalid data");
            Ingredient ingredient = new Ingredient()
            {
                Name = ingredientdto.Name,
                Measurement = ingredientdto.Measurement,
                IngredientValues = new List<IngredientValue>()
            };
            context.Ingredients.Add(ingredient);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("GetIngredientsList")]
        public async Task<ActionResult<List<IngredientDTO>>> GetIngredientsList()
        {
            List<IngredientDTO> result = new List<IngredientDTO>(); 
            foreach (var i in context.Ingredients)
            {
                IngredientDTO ingredient = new IngredientDTO()
                {
                    Id= i.Id,
                    Name = i.Name,
                    Measurement = i.Measurement
                };
                result.Add(ingredient);
            }
            return Ok(result);
        }

        [HttpGet("GetIngredientById")]
        public async Task<ActionResult<IngredientDTO>> GetIngredientById(int id)
        {
            if (id==0) return BadRequest("Invalid data");
            Ingredient ingredient= await context.Ingredients.FirstOrDefaultAsync(x => x.Id==id);
            if (ingredient == null) return NotFound();
            IngredientDTO result = new IngredientDTO()
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Measurement = ingredient.Measurement
            };
            return Ok(result);
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

        //[HttpPost("CreateNewIngredientValue")]
        //public async Task<ActionResult> CreateNewIngredientValue(IngredientValueDTO ingredientValueDTO)
        //{

        //}
    }
}
