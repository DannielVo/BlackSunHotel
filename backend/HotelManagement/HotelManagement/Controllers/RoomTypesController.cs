using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelManagement.Models;

[ApiController]
[Route("api/[controller]")]
public class RoomTypesController : ControllerBase
{
    private readonly HotelSQL _context;
    public RoomTypesController(HotelSQL context) {
        _context = context;
    }

    // GET: api/RoomTypes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomType>>> GetRoomTypes()
    {
        // Returns all room types, including RoomTypeName in the JSON
        return await _context.RoomTypes.ToListAsync();
    }

    // GET: api/RoomTypes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<RoomType>> GetRoomType(int id)
    {
        var roomType = await _context.RoomTypes.FindAsync(id);
        if (roomType == null) return NotFound();
        return roomType;  // JSON will include roomTypeName
    }

    // POST: api/RoomTypes
    [HttpPost]
    public async Task<ActionResult<RoomType>> PostRoomType([FromBody] RoomType roomType)
    {
        // roomType.RoomTypeName will be bound from the request JSON
        _context.RoomTypes.Add(roomType);
        await _context.SaveChangesAsync();

        // Return the created resource (including the new property)
        return CreatedAtAction(nameof(GetRoomType), new { id = roomType.RoomTypeId }, roomType);
    }

    // PUT: api/RoomTypes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRoomType(int id, [FromBody] RoomType roomType)
    {
        if (id != roomType.RoomTypeId) return BadRequest();

        // Attach and mark modified, including RoomTypeName
        _context.Entry(roomType).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // (Optional) DELETE: api/RoomTypes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoomType(int id)
    {
        var roomType = await _context.RoomTypes.FindAsync(id);
        if (roomType == null) return NotFound();
        _context.RoomTypes.Remove(roomType);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
