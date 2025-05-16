using HotelManagement.DTOs;
using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class RoomTypesController : ControllerBase
{
    private readonly HotelSQL _context;

    public RoomTypesController(HotelSQL context)
    {
        _context = context;
    }

    // GET: api/RoomTypes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomTypeDTO>>> GetRoomTypes()
    {
        return await _context.RoomTypes
            .Select(rt => new RoomTypeDTO
            {
                RoomTypeId = rt.RoomTypeId,
                RoomDesc = rt.RoomDesc,
                RoomFeatures = rt.RoomFeatures,
                RoomAmenities = rt.RoomAmenities,
                RoomImg = rt.RoomImg,
                RoomPrice = rt.RoomPrice
            })
            .ToListAsync();
    }

    // GET: api/RoomTypes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<RoomTypeDTO>> GetRoomType(int id)
    {
        var roomType = await _context.RoomTypes
            .Where(rt => rt.RoomTypeId == id)
            .Select(rt => new RoomTypeDTO
            {
                RoomTypeId = rt.RoomTypeId,
                RoomDesc = rt.RoomDesc,
                RoomFeatures = rt.RoomFeatures,
                RoomAmenities = rt.RoomAmenities,
                RoomImg = rt.RoomImg,
                RoomPrice = rt.RoomPrice
            })
            .FirstOrDefaultAsync();

        if (roomType == null) return NotFound();
        return roomType;
    }

    // POST: api/RoomTypes
    [HttpPost]
    public async Task<ActionResult<RoomTypeDTO>> PostRoomType([FromBody] RoomTypeDTO RoomTypeDTO)
    {
        var roomType = new RoomType
        {
            RoomDesc = RoomTypeDTO.RoomDesc,
            RoomFeatures = RoomTypeDTO.RoomFeatures,
            RoomAmenities = RoomTypeDTO.RoomAmenities,
            RoomImg = RoomTypeDTO.RoomImg,
            RoomPrice = RoomTypeDTO.RoomPrice
        };

        _context.RoomTypes.Add(roomType);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRoomType),
            new { id = roomType.RoomTypeId },
            new RoomTypeDTO
            {
                RoomTypeId = roomType.RoomTypeId,
                RoomDesc = roomType.RoomDesc,
                RoomFeatures = roomType.RoomFeatures,
                RoomAmenities = roomType.RoomAmenities,
                RoomImg = roomType.RoomImg,
                RoomPrice = roomType.RoomPrice
            });
    }

    // PUT: api/RoomTypes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRoomType(int id, [FromBody] RoomTypeDTO RoomTypeDTO)
    {
        if (id != RoomTypeDTO.RoomTypeId) return BadRequest();

        var roomType = await _context.RoomTypes.FindAsync(id);
        if (roomType == null) return NotFound();

        roomType.RoomDesc = RoomTypeDTO.RoomDesc;
        roomType.RoomFeatures = RoomTypeDTO.RoomFeatures;
        roomType.RoomAmenities = RoomTypeDTO.RoomAmenities;
        roomType.RoomImg = RoomTypeDTO.RoomImg;
        roomType.RoomPrice = RoomTypeDTO.RoomPrice;

        _context.Entry(roomType).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/RoomTypes/5
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