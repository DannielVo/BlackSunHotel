using HotelManagement.DTOs;
using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParkingServicesController : ControllerBase
    {
        private readonly HotelSQL _context;
        private readonly ILogger<ParkingServicesController> _logger;

        public ParkingServicesController(HotelSQL context, ILogger<ParkingServicesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/parkingservices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingServiceResponseDTO>>> GetParkingServices()
        {
            return await _context.ParkingServices
                .Select(ps => new ParkingServiceResponseDTO
                {
                    ParkingServiceId = ps.ParkingServiceId,
                    BookingId = ps.BookingId,
                    ParkingPlateNo = ps.ParkingPlateNo
                })
                .ToListAsync();
        }

        // GET: api/parkingservices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingServiceResponseDTO>> GetParkingService(int id)
        {
            var parkingService = await _context.ParkingServices
                .Where(ps => ps.ParkingServiceId == id)
                .Select(ps => new ParkingServiceResponseDTO
                {
                    ParkingServiceId = ps.ParkingServiceId,
                    BookingId = ps.BookingId,
                    ParkingPlateNo = ps.ParkingPlateNo
                })
                .FirstOrDefaultAsync();

            if (parkingService == null)
            {
                return NotFound(new ErrorResponseDTO
                {
                    StatusCode = 404,
                    Message = "Parking service record not found"
                });
            }
            return parkingService;
        }

        // POST: api/parkingservices
        [HttpPost]
        public async Task<ActionResult<ParkingServiceResponseDTO>> PostParkingService(
            [FromBody] CreateParkingServiceDTO createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResponseDTO
                {
                    StatusCode = 400,
                    Message = "Invalid request data"
                });
            }

            // Validate booking exists
            if (!await _context.Bookings.AnyAsync(b => b.BookingId == createDto.BookingId))
            {
                return BadRequest(new ErrorResponseDTO
                {
                    StatusCode = 400,
                    Message = "Invalid Booking ID"
                });
            }

            var parkingService = new ParkingService
            {
                BookingId = createDto.BookingId,
                ParkingPlateNo = createDto.ParkingPlateNo
            };

            try
            {
                _context.ParkingServices.Add(parkingService);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to create parking service");
                return StatusCode(500, new ErrorResponseDTO
                {
                    StatusCode = 500,
                    Message = "Failed to save parking service"
                });
            }

            return CreatedAtAction(nameof(GetParkingService),
                new { id = parkingService.ParkingServiceId },
                new ParkingServiceResponseDTO
                {
                    ParkingServiceId = parkingService.ParkingServiceId,
                    BookingId = parkingService.BookingId,
                    ParkingPlateNo = parkingService.ParkingPlateNo
                });
        }

        // PUT: api/parkingservices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParkingService(int id, [FromBody] UpdateParkingServiceDTO updateDto)
        {
            if (!ModelState.IsValid || id != updateDto.ParkingServiceId)
            {
                return BadRequest(new ErrorResponseDTO
                {
                    StatusCode = 400,
                    Message = "Invalid request data"
                });
            }

            var parkingService = await _context.ParkingServices.FindAsync(id);
            if (parkingService == null)
            {
                return NotFound(new ErrorResponseDTO
                {
                    StatusCode = 404,
                    Message = "Parking service record not found"
                });
            }

            // Update only provided fields
            if (!string.IsNullOrEmpty(updateDto.ParkingPlateNo))
                parkingService.ParkingPlateNo = updateDto.ParkingPlateNo;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrency error updating parking service {id}", id);
                if (!ParkingServiceExists(id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/parkingservices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParkingService(int id)
        {
            var parkingService = await _context.ParkingServices.FindAsync(id);
            if (parkingService == null)
            {
                return NotFound(new ErrorResponseDTO
                {
                    StatusCode = 404,
                    Message = "Parking service record not found"
                });
            }

            try
            {
                _context.ParkingServices.Remove(parkingService);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to delete parking service {id}", id);
                return StatusCode(500, new ErrorResponseDTO
                {
                    StatusCode = 500,
                    Message = "Failed to delete parking service"
                });
            }

            return NoContent();
        }

        private bool ParkingServiceExists(int id)
        {
            return _context.ParkingServices.Any(e => e.ParkingServiceId == id);
        }
    }
}