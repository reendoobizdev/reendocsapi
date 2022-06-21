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
            var query = from person in _context.People
                        join departmen in _context.Departments on person.DepartmentId equals departmen.Id
                        select new
                        {
                            person.Id,
                            person.FullName,
                            person.Photo,
                            person.UserId,
                            person.PositionId,
                            departmen.Name
                        };

            var result = await query.ToListAsync();
            return Ok(result);
            //return Ok(await _context.People.ToListAsync());
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
        [HttpPut("updatePeople/{id}")]
        public async Task<ActionResult<List<Person>>> updatePeople(int id ,[FromBody]Person person)
        {
            var dbPeople = await _context.People.FindAsync(id);
            if (dbPeople == null)
                return BadRequest("User not found.");
            dbPeople.FullName = person.FullName;
            dbPeople.DepartmentId = person.DepartmentId;
            dbPeople.PositionId = person.PositionId;
            dbPeople.Email = person.Email;
            dbPeople.Phone = person.Phone;
            dbPeople.Photo = person.Photo;
            await _context.SaveChangesAsync();
            return Ok("Update Success");

        }
        [HttpPut("{id}")]
        public async Task<ActionResult<List<Person>>> setToUser(int id)
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
