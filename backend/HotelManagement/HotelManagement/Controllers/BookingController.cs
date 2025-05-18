using HotelManagement.DTOs;
using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly HotelSQL _context;
        private readonly ILogger<BookingsController> _logger;

        public BookingsController(HotelSQL context, ILogger<BookingsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingResponseDTO>>> GetBookings()
        {
            return await _context.Bookings
                .Include(b => b.BookingDetails)
                .ThenInclude(bd => bd.Room)
                .ThenInclude(r => r.RoomType)
                .Select(b => new BookingResponseDTO
                {
                    BookingId = b.BookingId,
                    UserId = b.UserId,
                    Fullname = b.Fullname,
                    Email = b.Email,
                    Phone = b.Phone,
                    CheckInDate = b.CheckInDate,
                    CheckOutDate = b.CheckOutDate,
                    TotalPrice = b.TotalPrice,
                    PaymentStatus = b.PaymentStatus,
                    BookingDetails = b.BookingDetails.Select(bd => new BookingDetailResponseDTO
                    {
                        RoomId = bd.RoomId,
                        RoomTitle = bd.Room.RoomTitle,
                        RoomType = bd.Room.RoomType.RoomDesc,
                        PricePerNight = bd.Room.RoomType.RoomPrice // Added
                    }).ToList()
                })
                .ToListAsync();
        }

        // GET: api/bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingResponseDTO>> GetBooking(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.BookingDetails)
                .ThenInclude(bd => bd.Room)
                .ThenInclude(r => r.RoomType)
                .FirstOrDefaultAsync(b => b.BookingId == id);

            if (booking == null) return NotFound(new ErrorResponseDTO { StatusCode = 404, Message = "Booking not found" });

            return new BookingResponseDTO
            {
                BookingId = booking.BookingId,
                UserId = booking.UserId,
                Fullname = booking.Fullname,
                Email = booking.Email,
                Phone = booking.Phone,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                TotalPrice = booking.TotalPrice,
                PaymentStatus = booking.PaymentStatus,
                BookingDetails = booking.BookingDetails.Select(bd => new BookingDetailResponseDTO
                {
                    RoomId = bd.RoomId,
                    RoomTitle = bd.Room.RoomTitle,
                    RoomType = bd.Room.RoomType.RoomDesc,
                    PricePerNight = bd.Room.RoomType.RoomPrice // Added
                }).ToList()
            };
        }

        // POST: api/bookings
        [HttpPost]
        public async Task<ActionResult<BookingResponseDTO>> CreateBooking([FromBody] CreateBookingDTO createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorResponseDTO { StatusCode = 400, Message = "Invalid request data" });

            // Validate user exists
            if (!await _context.Users.AnyAsync(u => u.UserId == createDto.UserId))
                return BadRequest(new ErrorResponseDTO { StatusCode = 400, Message = "User not found" });

            var booking = new Booking
            {
                UserId = createDto.UserId,
                Fullname = createDto.Fullname,
                Email = createDto.Email,
                Phone = createDto.Phone,
                CheckInDate = createDto.CheckInDate,
                CheckOutDate = createDto.CheckOutDate,
                PaymentStatus = "pending"
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBooking), new { id = booking.BookingId }, booking);
        }

        // PUT: api/bookings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] UpdateBookingDTO updateDto)
        {
            if (!ModelState.IsValid || id != updateDto.BookingId)
                return BadRequest(new ErrorResponseDTO { StatusCode = 400, Message = "Invalid request data" });

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound();

            // Update only provided fields
            if (updateDto.Fullname != null) booking.Fullname = updateDto.Fullname;
            if (updateDto.Email != null) booking.Email = updateDto.Email;
            if (updateDto.Phone != null) booking.Phone = updateDto.Phone;
            if (updateDto.CheckInDate.HasValue) booking.CheckInDate = updateDto.CheckInDate.Value;
            if (updateDto.CheckOutDate.HasValue) booking.CheckOutDate = updateDto.CheckOutDate.Value;
            if (updateDto.TotalPrice.HasValue) booking.TotalPrice = updateDto.TotalPrice.Value;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrency error updating booking {id}", id);
                if (!BookingExists(id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.BookingDetails)
                .Include(b => b.ParkingServices)
                .Include(b => b.Reviews)
                .FirstOrDefaultAsync(b => b.BookingId == id);

            if (booking == null) return NotFound();

            if (booking.BookingDetails.Any() || booking.ParkingServices.Any() || booking.Reviews.Any())
                return BadRequest(new ErrorResponseDTO { StatusCode = 400, Message = "Booking has dependent records" });

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/bookings/check-availability
        [HttpGet("check-availability")]
        public async Task<ActionResult<IEnumerable<AvailableRoomResponseDTO>>> CheckAvailability(
            [FromQuery] CheckAvailabilityRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorResponseDTO { StatusCode = 400, Message = "Invalid date range" });

            var bookedRoomIds = await _context.BookingDetails
                .Where(bd => bd.Booking.CheckInDate < request.CheckOut &&
                            bd.Booking.CheckOutDate > request.CheckIn)
                .Select(bd => bd.RoomId)
                .Distinct()
                .ToListAsync();

            var availableRooms = await _context.Rooms
                .Include(r => r.RoomType)
                .Where(r => !bookedRoomIds.Contains(r.RoomId) && r.RoomStatus == "available")
                .Select(r => new AvailableRoomResponseDTO
                {
                    RoomId = r.RoomId,
                    RoomTitle = r.RoomTitle,
                    RoomDescription = r.RoomDescription,
                    RoomType = r.RoomType.RoomDesc,
                    PricePerNight = r.RoomType.RoomPrice
                })
                .ToListAsync();

            return Ok(availableRooms);
        }

        // POST: api/bookings/confirm
        [HttpPost("confirm")]
        public async Task<ActionResult<ConfirmBookingResponseDTO>> ConfirmBooking([FromBody] ConfirmBookingDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorResponseDTO { StatusCode = 400, Message = "Invalid booking data" });

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Check room existence
                var invalidRooms = request.RoomIds
                    .Where(id => !_context.Rooms.Any(r => r.RoomId == id))
                    .ToList();

                if (invalidRooms.Any())
                    return BadRequest(new ErrorResponseDTO
                    {
                        StatusCode = 400,
                        Message = $"Invalid rooms: {string.Join(",", invalidRooms)}"
                    });

                // Re-check availability
                var bookedRoomIds = await _context.BookingDetails
                    .Where(bd => bd.Booking.CheckInDate < request.CheckOutDate &&
                                 bd.Booking.CheckOutDate > request.CheckInDate)
                    .Select(bd => bd.RoomId)
                    .ToListAsync();

                var overlappingRooms = request.RoomIds.Intersect(bookedRoomIds).ToList();
                if (overlappingRooms.Any())
                {
                    await transaction.RollbackAsync();
                    return BadRequest(new ErrorResponseDTO
                    {
                        StatusCode = 400,
                        Message = $"Rooms {string.Join(",", overlappingRooms)} are booked"
                    });
                }

                // Calculate total price
                var rooms = await _context.Rooms
                    .Include(r => r.RoomType)
                    .Where(r => request.RoomIds.Contains(r.RoomId))
                    .ToListAsync();

                var nights = (request.CheckOutDate.DayNumber - request.CheckInDate.DayNumber);
                var totalPrice = rooms.Sum(r => r.RoomType.RoomPrice * nights);

                // Create booking
                var booking = new Booking
                {
                    UserId = request.UserId,
                    Fullname = request.Fullname,
                    Email = request.Email,
                    Phone = request.Phone,
                    CheckInDate = request.CheckInDate,
                    CheckOutDate = request.CheckOutDate,
                    TotalPrice = totalPrice, // Server-calculated
                    PaymentStatus = "confirmed" // Auto-confirm for simplicity
                };

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                // Add booking details
                _context.BookingDetails.AddRange(request.RoomIds.Select(roomId => new BookingDetail
                {
                    BookingId = booking.BookingId,
                    RoomId = roomId
                }));

                // Update room status
                foreach (var room in rooms)
                {
                    room.RoomStatus = "occupied";
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new ConfirmBookingResponseDTO
                {
                    Message = "Booking confirmed",
                    BookingId = booking.BookingId,
                    TotalPrice = totalPrice,
                    BookedRoomIds = request.RoomIds
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Failed to confirm booking");
                return StatusCode(500, new ErrorResponseDTO
                {
                    StatusCode = 500,
                    Message = "Booking failed. Please try again."
                });
            }
        }

        private bool BookingExists(int id) => _context.Bookings.Any(e => e.BookingId == id);
    }
}