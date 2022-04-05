using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReenDocsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly DataContext _context;

        public RoleController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Role>>> Get()
        {
            return Ok(await _context.Roles.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> Get(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
                return BadRequest("Role not found.");
            return Ok(role);
        }

        [HttpPost]
        public async Task<ActionResult<List<Role>>> AddRole(Role Role)
        {
            _context.Roles.Add(Role);
            await _context.SaveChangesAsync();

            return Ok(await _context.Roles.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Role>>> UpdateHero(Role newRole)
        {
            var dbRole = await _context.Roles.FindAsync(newRole.Id);
            if (dbRole == null)
                return BadRequest("Role not found.");

            dbRole.Name = newRole.Name;
            dbRole.Access = newRole.Access;

            await _context.SaveChangesAsync();

            return Ok(await _context.Roles.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Role>>> Delete(int id)
        {
            var dbRoles = await _context.Roles.FindAsync(id);
            if (dbRoles == null)
                return BadRequest("Hero not found.");

            _context.Roles.Remove(dbRoles);
            await _context.SaveChangesAsync();

            return Ok(await _context.Roles.ToListAsync());
        }

    }
}
