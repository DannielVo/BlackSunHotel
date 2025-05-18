using HotelManagement.DTOs;
using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly HotelSQL _context;
    private readonly ILogger<RoomsController> _logger;

    public RoomsController(HotelSQL context, ILogger<RoomsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
    {
        return await _context.Rooms
            .Include(r => r.RoomType)
            .Select(r => new RoomDTO
            {
                RoomId = r.RoomId,
                RoomTitle = r.RoomTitle,
                RoomTypeId = r.RoomTypeId,
                RoomDescription = r.RoomDescription,
                RoomImage = r.RoomImage,
                RoomStatus = r.RoomStatus,
                RoomTypeName = r.RoomType.RoomTypeName,
                RoomTypePrice = r.RoomType.RoomPrice
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoomDTO>> GetRoom(int id)
    {
        var room = await _context.Rooms
            .Include(r => r.RoomType)
            .Where(r => r.RoomId == id)
            .Select(r => new RoomDTO
            {
                RoomId = r.RoomId,
                RoomTitle = r.RoomTitle,
                RoomTypeId = r.RoomTypeId,
                RoomDescription = r.RoomDescription,
                RoomImage = r.RoomImage,
                RoomStatus = r.RoomStatus,
                RoomTypeName = r.RoomType.RoomTypeName,
                RoomTypePrice = r.RoomType.RoomPrice
            })
            .FirstOrDefaultAsync();

        if (room == null) return NotFound();
        return room;
    }

    [HttpPost]
    public async Task<ActionResult<RoomDTO>> PostRoom([FromBody] RoomDTO roomDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ErrorResponseDTO
            {
                StatusCode = 400,
                Message = "Invalid room data"
            });
        }

        // Validate RoomTypeId exists
        var roomTypeExists = await _context.RoomTypes.AnyAsync(rt => rt.RoomTypeId == roomDto.RoomTypeId);
        if (!roomTypeExists)
        {
            return BadRequest(new ErrorResponseDTO
            {
                StatusCode = 400,
                Message = "Invalid RoomTypeId"
            });
        }

        var room = new Room
        {
            RoomTitle = roomDto.RoomTitle,
            RoomTypeId = roomDto.RoomTypeId,
            RoomDescription = roomDto.RoomDescription,
            RoomImage = roomDto.RoomImage,
            RoomStatus = roomDto.RoomStatus
        };

        try
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Failed to create room");
            return StatusCode(500, new ErrorResponseDTO
            {
                StatusCode = 500,
                Message = "Failed to save room"
            });
        }

        return CreatedAtAction(nameof(GetRoom), new { id = room.RoomId }, roomDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutRoom(int id, [FromBody] RoomDTO roomDto)
    {
        if (!ModelState.IsValid || id != roomDto.RoomId)
        {
            return BadRequest(new ErrorResponseDTO
            {
                StatusCode = 400,
                Message = "Invalid request"
            });
        }

        var room = await _context.Rooms.FindAsync(id);
        if (room == null) return NotFound();

        // Validate RoomTypeId exists
        var roomTypeExists = await _context.RoomTypes.AnyAsync(rt => rt.RoomTypeId == roomDto.RoomTypeId);
        if (!roomTypeExists)
        {
            return BadRequest(new ErrorResponseDTO
            {
                StatusCode = 400,
                Message = "Invalid RoomTypeId"
            });
        }

        room.RoomTitle = roomDto.RoomTitle;
        room.RoomTypeId = roomDto.RoomTypeId;
        room.RoomDescription = roomDto.RoomDescription;
        room.RoomImage = roomDto.RoomImage;
        room.RoomStatus = roomDto.RoomStatus;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RoomExists(id)) return NotFound();
            throw;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Failed to update room");
            return StatusCode(500, new ErrorResponseDTO
            {
                StatusCode = 500,
                Message = "Failed to save changes"
            });
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        var room = await _context.Rooms
            .Include(r => r.BookingDetails)
            .Include(r => r.Reviews)
            .FirstOrDefaultAsync(r => r.RoomId == id);

        if (room == null) return NotFound();

        if (room.BookingDetails.Any() || room.Reviews.Any())
        {
            return BadRequest(new ErrorResponseDTO
            {
                StatusCode = 400,
                Message = "Cannot delete: Room has bookings or reviews"
            });
        }

        try
        {
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Failed to delete room");
            return StatusCode(500, new ErrorResponseDTO
            {
                StatusCode = 500,
                Message = "Failed to delete room"
            });
        }

        return NoContent();
    }

    private bool RoomExists(int id)
    {
        return _context.Rooms.Any(e => e.RoomId == id);
    }
}