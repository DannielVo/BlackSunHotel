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
    public class RoomServicesController : ControllerBase
    {
        private readonly HotelSQL _context;

        public RoomServicesController(HotelSQL context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomService>>> GetRoomServices()
        {
            return await _context.RoomServices.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomService>> GetRoomService(int id)
        {
            var roomService = await _context.RoomServices.FindAsync(id);
            if (roomService == null)
            {
                return NotFound();
            }
            return roomService;
        }

        [HttpPost]
        public async Task<ActionResult<RoomService>> PostRoomService(RoomService roomService)
        {
            _context.RoomServices.Add(roomService);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRoomService), new { id = roomService.RoomServiceId }, roomService);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomService(int id, RoomService roomService)
        {
            if (id != roomService.RoomServiceId)
            {
                return BadRequest();
            }
            _context.Entry(roomService).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.RoomServices.Any(e => e.RoomServiceId == id))
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
        public async Task<IActionResult> DeleteRoomService(int id)
        {
            var roomService = await _context.RoomServices.FindAsync(id);
            if (roomService == null)
            {
                return NotFound();
            }
            _context.RoomServices.Remove(roomService);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
