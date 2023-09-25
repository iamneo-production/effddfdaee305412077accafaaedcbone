using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetmicroservicetwo.Models;

namespace dotnetmicroservicetwo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly AirportDbContext _context;

        public AirportController(AirportDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Airport>>> GetAllAirports()
        {
            var Airports = await _context.Airports.ToListAsync();
            return Ok(Airports);
        }
[HttpGet("AirportNames")]
public async Task<ActionResult<IEnumerable<string>>> Get()
{
    // Project the AirportTitle property using Select
    var AirportNames = await _context.Airports
        .OrderBy(x => x.AirportName)
        .Select(x => x.AirportName)
        .ToListAsync();

    return AirportNames;
}
        [HttpPost]
        public async Task<ActionResult> AddAirport(Airport Airport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return detailed validation errors
            }
            await _context.Airports.AddAsync(Airport);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirport(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid Airport id");

            var Airport = await _context.Airports.FindAsync(id);
              _context.Airports.Remove(Airport);
                await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
