using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReenDocsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly DataContext _context;

        public PeopleController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Person>>> Get()
        {
            return Ok(await _context.People.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person == null)
                return BadRequest("Person not found.");
            return Ok(person);
        }

        [HttpPost]
        public async Task<ActionResult<List<Person>>> AddPerson(Person person)
        {
            _context.People.Add(person);
            await _context.SaveChangesAsync();

            return Ok(await _context.People.ToListAsync());
        }
       
        [HttpPut]
        public async Task<ActionResult<List<Person>>> UpdatePerson(int id)
        {
            var dbPeople = await _context.People.FindAsync(id);

            if (dbPeople == null)
                return BadRequest("User not found.");
            var username = dbPeople.FullName.Split(" ");
            var newUser = new User()
            {
                UserName = (username[0] + "123456"),
                Password = "123456"
            };

            var user = _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            dbPeople.UserId = user.Entity.Id;
            await _context.SaveChangesAsync();
            return Ok(await _context.People.ToListAsync());

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Person>>> Delete(int id)
        {
            var dbPeople = await _context.People.FindAsync(id);
            if (dbPeople == null)
                return BadRequest("Hero not found.");

            _context.People.Remove(dbPeople);
            await _context.SaveChangesAsync();

            return Ok(await _context.People.ToListAsync());
        }

    }
}
