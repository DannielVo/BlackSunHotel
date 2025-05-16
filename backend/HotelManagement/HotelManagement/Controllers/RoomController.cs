using HotelManagement.DTOs;
using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly HotelSQL _context;

    public RoomController(HotelSQL context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
    {
        return await _context.Rooms
            .Select(r => new RoomDTO
            {
                RoomId = r.RoomId,
                RoomTitle = r.RoomTitle,
                RoomTypeId = r.RoomTypeId,
                RoomDescription = r.RoomDescription,
                RoomImage = r.RoomImage,
                RoomStatus = r.RoomStatus
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoomDTO>> GetRoom(int id)
    {
        var room = await _context.Rooms
            .Where(r => r.RoomId == id)
            .Select(r => new RoomDTO
            {
                RoomId = r.RoomId,
                RoomTitle = r.RoomTitle,
                RoomTypeId = r.RoomTypeId,
                RoomDescription = r.RoomDescription,
                RoomImage = r.RoomImage,
                RoomStatus = r.RoomStatus
            })
            .FirstOrDefaultAsync();
        if (room == null)
            return NotFound();
        return room;
    }

    [HttpPost]
    public async Task<ActionResult<RoomDTO>> PostRoom([FromBody] RoomDTO RoomDTO)
    {
        var room = new Room
        {
            RoomTitle = RoomDTO.RoomTitle,
            RoomTypeId = RoomDTO.RoomTypeId,
            RoomDescription = RoomDTO.RoomDescription,
            RoomImage = RoomDTO.RoomImage,
            RoomStatus = RoomDTO.RoomStatus
        };

        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRoom), new { id = room.RoomId }, RoomDTO);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutRoom(int id, [FromBody] RoomDTO RoomDTO)
    {
        var room = await _context.Rooms.FindAsync(id);
        if (room == null)
            return NotFound();

        room.RoomTitle = RoomDTO.RoomTitle;
        room.RoomTypeId = RoomDTO.RoomTypeId;
        room.RoomDescription = RoomDTO.RoomDescription;
        room.RoomImage = RoomDTO.RoomImage;
        room.RoomStatus = RoomDTO.RoomStatus;

        _context.Entry(room).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        var room = await _context.Rooms.FindAsync(id);
        if (room == null)
            return NotFound();

        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}