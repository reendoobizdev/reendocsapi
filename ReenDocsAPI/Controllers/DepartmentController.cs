using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReenDocsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DataContext _context;

        public DepartmentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Department>>> Get()
        {
            return Ok(await _context.Departments.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> Get(int id)
        {
            var dept = await _context.Departments.FindAsync(id);
            if (dept == null)
                return BadRequest("Department not found.");
            return Ok(dept);
        }

        [HttpPost]
        public async Task<ActionResult<List<Department>>> AddDepartment(Department dept)
        {
            _context.Departments.Add(dept);
            await _context.SaveChangesAsync();
            return Ok(await _context.Departments.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Department>>> UpdateDepartment(Department newDept)
        {
            var dbDept = await _context.Departments.FindAsync(newDept.Id);
            if (dbDept == null)
                return BadRequest("Department not found.");
            dbDept.Name = newDept.Name;
            await _context.SaveChangesAsync();
            return Ok(await _context.Departments.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Department>>> Delete(int id)
        {
            var dbDept = await _context.Departments.FindAsync(id);
            if (dbDept == null)
                return BadRequest("Department not found.");

            _context.Departments.Remove(dbDept);
            await _context.SaveChangesAsync();

            return Ok(await _context.Departments.ToListAsync());
        }

    }
}
