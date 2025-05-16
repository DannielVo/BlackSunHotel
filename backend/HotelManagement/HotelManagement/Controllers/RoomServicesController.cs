using HotelManagement.DTOs;
using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<ActionResult<IEnumerable<RoomServicesDTO>>> GetRoomServices()
    {
        return await _context.RoomServices
            .Select(rs => new RoomServicesDTO
            {
                RoomServiceId = rs.RoomServiceId,
                RoomId = rs.RoomId,
                ServiceDateTime = rs.ServiceDateTime,
                RoomServiceStatus = rs.RoomServiceStatus
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoomServicesDTO>> GetRoomService(int id)
    {
        var roomService = await _context.RoomServices
            .Where(rs => rs.RoomServiceId == id)
            .Select(rs => new RoomServicesDTO
            {
                RoomServiceId = rs.RoomServiceId,
                RoomId = rs.RoomId,
                ServiceDateTime = rs.ServiceDateTime,
                RoomServiceStatus = rs.RoomServiceStatus
            })
            .FirstOrDefaultAsync();
        if (roomService == null) return NotFound();
        return roomService;
    }

    [HttpPost]
    public async Task<ActionResult<RoomServicesDTO>> PostRoomService([FromBody] RoomServicesDTO dto)
    {
        var newService = new RoomService
        {
            RoomId = dto.RoomId,
            ServiceDateTime = dto.ServiceDateTime,
            RoomServiceStatus = dto.RoomServiceStatus
        };

        _context.RoomServices.Add(newService);
        await _context.SaveChangesAsync();

        dto.RoomServiceId = newService.RoomServiceId;
        return CreatedAtAction(nameof(GetRoomService), new { id = dto.RoomServiceId }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutRoomService(int id, [FromBody] RoomServicesDTO dto)
    {
        if (id != dto.RoomServiceId) return BadRequest();

        var existingService = await _context.RoomServices.FindAsync(id);
        if (existingService == null) return NotFound();

        existingService.RoomId = dto.RoomId;
        existingService.ServiceDateTime = dto.ServiceDateTime;
        existingService.RoomServiceStatus = dto.RoomServiceStatus;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.RoomServices.Any(e => e.RoomServiceId == id))
                return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoomService(int id)
    {
        var roomService = await _context.RoomServices.FindAsync(id);
        if (roomService == null) return NotFound();

        _context.RoomServices.Remove(roomService);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}