using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReenDocsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly DataContext _context;

        public PositionController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Position>>> Get()
        {
            return Ok(await _context.Positions.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Position>> Get(int id)
        {
            var data = await _context.Positions.FindAsync(id);
            if (data == null)
                return BadRequest("Position not found.");
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<List<Position>>> AddPosition(Position Position)
        {
            _context.Positions.Add(Position);
            await _context.SaveChangesAsync();

            return Ok(await _context.Positions.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Position>>>UpdatePosition(Position newPosition)
        {
            var dbPosition = await _context.Positions.FindAsync(newPosition.Id);
            if (dbPosition == null)
                return BadRequest("Position not found.");

            dbPosition.Name = newPosition.Name;
            

            await _context.SaveChangesAsync();

            return Ok(await _context.Positions.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Position>>> Delete(int id)
        {
            var dbPositions = await _context.Positions.FindAsync(id);
            if (dbPositions == null)
                return BadRequest("Hero not found.");

            _context.Positions.Remove(dbPositions);
            await _context.SaveChangesAsync();

            return Ok(await _context.Positions.ToListAsync());
        }

    }
}
