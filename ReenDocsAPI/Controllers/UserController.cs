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
        public async Task<ActionResult<List<Person>>> Get()
        {
            return Ok(await _context.People.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            var hero = await _context.People.FindAsync(id);
            if (hero == null)
                return BadRequest("Person not found.");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<Person>>> AddPerson(Person person)
        {
            _context.People.Add(person);
            await _context.SaveChangesAsync();

            return Ok(await _context.People.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Person>>> UpdateHero(Person newPerson)
        {
            var dbPerson = await _context.People.FindAsync(newPerson.Id);
            if (dbPerson == null)
                return BadRequest("Person not found.");

            dbPerson.FullName = newPerson.FullName;
            dbPerson.Email = newPerson.Email;
            dbPerson.Photo = newPerson.Photo;
            dbPerson.Phone = newPerson.Phone;
            dbPerson.PositionId = newPerson.PositionId;
            dbPerson.DepartmentId = newPerson.DepartmentId;

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
