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

        public BookingsController(HotelSQL context)
        {
            _context = context;
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
                        RoomType = bd.Room.RoomType.RoomDesc
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

            if (booking == null)
            {
                return NotFound(new ErrorResponseDTO { StatusCode = 404, Message = "Booking not found" });
            }

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
                    RoomType = bd.Room.RoomType.RoomDesc
                }).ToList()
            };
        }

        // POST: api/bookings
        [HttpPost]
        public async Task<ActionResult<BookingResponseDTO>> CreateBooking([FromBody] CreateBookingDTO createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResponseDTO { StatusCode = 400, Message = "Invalid request data" });
            }

            // Check if user exists
            var userExists = await _context.Users.AnyAsync(u => u.UserId == createDto.UserId);
            if (!userExists)
            {
                return BadRequest(new ErrorResponseDTO { StatusCode = 400, Message = "User does not exist" });
            }

            var booking = new Booking
            {
                UserId = createDto.UserId,
                Fullname = createDto.Fullname,
                Email = createDto.Email,
                Phone = createDto.Phone,
                CheckInDate = createDto.CheckInDate,
                CheckOutDate = createDto.CheckOutDate,
                TotalPrice = createDto.TotalPrice,
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
            {
                return BadRequest(new ErrorResponseDTO { StatusCode = 400, Message = "Invalid request data" });
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound(new ErrorResponseDTO { StatusCode = 404, Message = "Booking not found" });
            }

            // Partial updates
            booking.Fullname = updateDto.Fullname ?? booking.Fullname;
            booking.Email = updateDto.Email ?? booking.Email;
            booking.Phone = updateDto.Phone ?? booking.Phone;
            booking.CheckInDate = updateDto.CheckInDate ?? booking.CheckInDate;
            booking.CheckOutDate = updateDto.CheckOutDate ?? booking.CheckOutDate;
            booking.TotalPrice = updateDto.TotalPrice ?? booking.TotalPrice;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
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

            if (booking == null)
            {
                return NotFound(new ErrorResponseDTO { StatusCode = 404, Message = "Booking not found" });
            }

            if (booking.BookingDetails.Any() || booking.ParkingServices.Any() || booking.Reviews.Any())
            {
                return BadRequest(new ErrorResponseDTO
                {
                    StatusCode = 400,
                    Message = "Cannot delete: Booking has dependent records."
                });
            }

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
            {
                return BadRequest(new ErrorResponseDTO { StatusCode = 400, Message = "Invalid date range" });
            }

            var bookedRoomIds = await _context.BookingDetails
                .Where(d => d.Booking.CheckInDate < request.CheckOut &&
                           d.Booking.CheckOutDate > request.CheckIn)
                .Select(d => d.RoomId)
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
                    PricePerNight = r.RoomType.RoomPrice,
                    RoomStatus = r.RoomStatus
                })
                .ToListAsync();

            return Ok(availableRooms);
        }

        // POST: api/bookings/confirm
        [HttpPost("confirm")]
        public async Task<ActionResult<ConfirmBookingResponseDTO>> ConfirmBooking(
            [FromBody] ConfirmBookingDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResponseDTO { StatusCode = 400, Message = "Invalid booking data" });
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Re-check room availability within the transaction
                var bookedRoomIds = await _context.BookingDetails
                    .Where(d => d.Booking.CheckInDate < request.CheckOutDate &&
                               d.Booking.CheckOutDate > request.CheckInDate)
                    .Select(d => d.RoomId)
                    .ToListAsync();

                var invalidRooms = request.RoomIds.Intersect(bookedRoomIds).ToList();
                if (invalidRooms.Any())
                {
                    await transaction.RollbackAsync();
                    return BadRequest(new ErrorResponseDTO
                    {
                        StatusCode = 400,
                        Message = $"Rooms {string.Join(",", invalidRooms)} are already booked."
                    });
                }

                // Create booking
                var booking = new Booking
                {
                    UserId = request.UserId,
                    Fullname = request.Fullname,
                    Email = request.Email,
                    Phone = request.Phone,
                    CheckInDate = request.CheckInDate,
                    CheckOutDate = request.CheckOutDate,
                    TotalPrice = request.TotalPrice,
                    PaymentStatus = "pending"
                };

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                // Add booking details
                foreach (var roomId in request.RoomIds)
                {
                    _context.BookingDetails.Add(new BookingDetail
                    {
                        BookingId = booking.BookingId,
                        RoomId = roomId
                    });
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new ConfirmBookingResponseDTO
                {
                    Message = "Booking confirmed successfully",
                    BookingId = booking.BookingId,
                    TotalPrice = booking.TotalPrice,
                    BookedRoomIds = request.RoomIds
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new ErrorResponseDTO
                {
                    StatusCode = 500,
                    Message = $"Booking failed: {ex.Message}"
                });
            }
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }
    }
}