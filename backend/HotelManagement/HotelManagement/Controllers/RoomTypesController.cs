using HotelManagement.DTOs;
using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

[ApiController]
[Route("[controller]")]
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
                RoomTypeName = rt.RoomTypeName,
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
                RoomTypeName = rt.RoomTypeName,
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
    public async Task<ActionResult<RoomTypeDTO>> PostRoomType([FromBody] RoomTypeDTO roomTypeDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ErrorResponseDTO { StatusCode = 400, Message = "Invalid data" });
        }

        var roomType = new RoomType
        {
            RoomTypeName = roomTypeDto.RoomTypeName,
            RoomDesc = roomTypeDto.RoomDesc,
            RoomFeatures = roomTypeDto.RoomFeatures,
            RoomAmenities = roomTypeDto.RoomAmenities,
            RoomImg = roomTypeDto.RoomImg,
            RoomPrice = roomTypeDto.RoomPrice
        };

        _context.RoomTypes.Add(roomType);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(new ErrorResponseDTO
            {
                StatusCode = 500,
                Message = $"Failed to create room type: {ex.InnerException?.Message}"
            });
        }

        return CreatedAtAction(nameof(GetRoomType), new { id = roomType.RoomTypeId }, roomTypeDto);
    }

    // PUT: api/RoomTypes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRoomType(int id, [FromBody] RoomTypeDTO roomTypeDto)
    {
        if (id != roomTypeDto.RoomTypeId || !ModelState.IsValid)
        {
            return BadRequest(new ErrorResponseDTO { StatusCode = 400, Message = "Invalid request" });
        }

        var roomType = await _context.RoomTypes.FindAsync(id);
        if (roomType == null)
        {
            return NotFound();
        }

        roomType.RoomTypeName = roomTypeDto.RoomTypeName;
        roomType.RoomDesc = roomTypeDto.RoomDesc;
        roomType.RoomFeatures = roomTypeDto.RoomFeatures;
        roomType.RoomAmenities = roomTypeDto.RoomAmenities;
        roomType.RoomImg = roomTypeDto.RoomImg;
        roomType.RoomPrice = roomTypeDto.RoomPrice;

        _context.Entry(roomType).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RoomTypeExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(new ErrorResponseDTO
            {
                StatusCode = 500,
                Message = $"Failed to update room type: {ex.InnerException?.Message}"
            });
        }

        return NoContent();
    }

    // DELETE: api/RoomTypes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoomType(int id)
    {
        var roomType = await _context.RoomTypes
            .Include(rt => rt.Rooms)
            .FirstOrDefaultAsync(rt => rt.RoomTypeId == id);

        if (roomType == null)
        {
            return NotFound();
        }

        if (roomType.Rooms.Any())
        {
            return BadRequest(new ErrorResponseDTO
            {
                StatusCode = 400,
                Message = "Cannot delete: Rooms are assigned to this type."
            });
        }

        _context.RoomTypes.Remove(roomType);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(new ErrorResponseDTO
            {
                StatusCode = 500,
                Message = $"Failed to delete room type: {ex.InnerException?.Message}"
            });
        }

        return NoContent();
    }

    private bool RoomTypeExists(int id)
    {
        return _context.RoomTypes.Any(e => e.RoomTypeId == id);
    }
}