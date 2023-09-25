using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetmicroserviceone.Models;

namespace dotnetmicroserviceone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly FlightDbContext _context;

        public FlightController(FlightDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> GetAllFlights()
        {
            var flights = await _context.Flights.ToListAsync();
            return Ok(flights);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetFlightById(int id)
        {
            var flight = await _context.Flights.FindAsync(id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }
        [HttpPost]
        public async Task<ActionResult> AddFlight(Flight flight)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return detailed validation errors
            }
            await _context.Flights.AddAsync(flight);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid Flight id");

            var flight = await _context.Flights.FindAsync(id);
              _context.Flights.Remove(flight);
                await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
