using System.Collections.Generic;
using System.Threading.Tasks;
using GPSCIService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GPSCIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoordinateController : ControllerBase
    {
        private readonly ModelContext _context;

        public CoordinateController(ModelContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coordinate>>> GetCoordinateItems()
        {
            return await _context.Coordinates.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Coordinate>> GetCoordinateItem(int id)
        {
            var Coordinat = await _context.Coordinates.FirstOrDefaultAsync(x=>x.IDCoordinate==id);
            if (Coordinat == null)  return NotFound();
            return Coordinat;
        }

        [HttpPost]
        public async Task<IActionResult> PostCoordinateItem([FromBody]Coordinate[] item)
        {
            _context.Coordinates.AddRange(item);
            await _context.SaveChangesAsync();
            return Ok(item);
        }
    }
}
