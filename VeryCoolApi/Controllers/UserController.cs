using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeryCoolApi.Model;

namespace VeryCoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly VeryCoolDbContext context;
        public UserController(VeryCoolDbContext context)
        {
            this.context = context;
        }

        [HttpGet("IfUserExist")]
        public async Task<ActionResult<bool>> IfUserExist(string login)
        {
            if (login==null)
                return BadRequest("Invalid data");
            User user= await context.Users.FirstOrDefaultAsync(x => x.Login==login);
            if (user==null) return Ok(false);
            return Ok(true);
        }

        [HttpGet("SignUserIn")]
        public async Task<ActionResult<bool>> SignUserIn(User user)
        {
            if (user == null)
                return BadRequest("Invalid data");
            User found_user= await context.Users.FirstOrDefaultAsync(u=>u.Login==user.Login);
            if (found_user==null) return NotFound();
            if (found_user.Password==user.Password) return Ok(true);
            else return Ok(false);
        }

        [HttpPost("CreateNewUser")]
        public async Task<ActionResult> CreateNewUser(User user)
        {
            if (user == null)
                return BadRequest("Invalid data");
            User found_user = await context.Users.FirstOrDefaultAsync(u => u.Login == user.Login);
            if (found_user != null) return BadRequest("User already exist");
            context.Add(user);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
