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
                IngredientValues = []
            };
            context.Ingredients.Add(ingredient);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("GetIngredientsList")]
        public async Task<ActionResult<List<IngredientDTO>>> GetIngredientsList()
        {
            List<IngredientDTO> result = new List<IngredientDTO>();
            var ingrediens = context.Ingredients.ToList();
            foreach (Ingredient i in ingrediens)
            {
                result.Add(new IngredientDTO()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Measurement = i.Measurement
                });
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

        [HttpPost("CreateNewIngredientValues")]
        public async Task<ActionResult> CreateNewIngredientValues(List<IngredientValueDTO> ingredientValueDTOList)
        {
            foreach (var ingredientValueDTO in ingredientValueDTOList)
            {
                IngredientValue ingredientValue = new IngredientValue()
                {
                    RecipeId = ingredientValueDTO.RecipeId,
                    IngredientId = ingredientValueDTO.IngredientId,
                    Quantity = ingredientValueDTO.Quantity,
                    Ingredient = context.Ingredients.FirstOrDefault(i => i.Id == ingredientValueDTO.IngredientId),
                    Recipe = context.Recipes.FirstOrDefault(r => r.Id == ingredientValueDTO.RecipeId)
                };
                context.IngredientValues.Add(ingredientValue);
            }
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("GetIngredientValuesByRecipeId")]
        public async Task<ActionResult<List<IngredientValueDTO>>> GetIngredientValuesByRecipeId(int recipe_id)
        {
            if (recipe_id == 0) return BadRequest("Invalid data");
            List<IngredientValueDTO> result= new List<IngredientValueDTO>();
            foreach (var i in context.IngredientValues)
            {
               if (i.RecipeId == recipe_id)
                {
                    result.Add(new IngredientValueDTO()
                    {
                        Id = i.Id,
                        RecipeId = i.RecipeId,
                        IngredientId = i.IngredientId,
                        Quantity = i.Quantity
                    });
                }
            }
            return Ok(result);
        }
    }
}
