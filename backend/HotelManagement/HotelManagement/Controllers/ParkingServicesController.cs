using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelManagement.Models;

namespace HotelManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingServicesController : ControllerBase
    {
        private readonly HotelSQL _context;

        public ParkingServicesController(HotelSQL context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingService>>> GetParkingServices()
        {
            return await _context.ParkingServices.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingService>> GetParkingService(int id)
        {
            var parkingService = await _context.ParkingServices.FindAsync(id);
            if (parkingService == null)
            {
                return NotFound();
            }
            return parkingService;
        }

        [HttpPost]
        public async Task<ActionResult<ParkingService>> PostParkingService(ParkingService parkingService)
        {
            _context.ParkingServices.Add(parkingService);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetParkingService), new { id = parkingService.ParkingServiceId }, parkingService);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutParkingService(int id, ParkingService parkingService)
        {
            if (id != parkingService.ParkingServiceId)
            {
                return BadRequest();
            }
            _context.Entry(parkingService).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.ParkingServices.Any(e => e.ParkingServiceId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParkingService(int id)
        {
            var parkingService = await _context.ParkingServices.FindAsync(id);
            if (parkingService == null)
            {
                return NotFound();
            }
            _context.ParkingServices.Remove(parkingService);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
