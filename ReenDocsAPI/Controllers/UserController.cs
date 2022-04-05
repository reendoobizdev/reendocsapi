using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReenDocsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var hero = await _context.Users.FindAsync(id);
            if (hero == null)
                return BadRequest("User not found.");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<User>>> AddUser(User User)
        {
            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<User>>> UpdateHero(User newUser)
        {
            var dbUser = await _context.Users.FindAsync(newUser.Id);
            if (dbUser == null)
                return BadRequest("User not found.");

            dbUser.UserName = newUser.UserName;
            dbUser.Password = newUser.Password;
            

            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<User>>> Delete(int id)
        {
            var dbUsers = await _context.Users.FindAsync(id);
            if (dbUsers == null)
                return BadRequest("Hero not found.");

            _context.Users.Remove(dbUsers);
            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }

    }
}
