using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelManagement.Models;
using HotelManagement.DTOs;

namespace HotelManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly HotelSQL _context;

        public BookingsController(HotelSQL context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            return await _context.Bookings.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return booking;
        }

        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBooking), new { id = booking.BookingId }, booking);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.BookingId)
            {
                return BadRequest();
            }
            _context.Entry(booking).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Bookings.Any(e => e.BookingId == id))
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
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("check-availability")]
        public async Task<IActionResult> CheckAvailability(DateOnly checkIn, DateOnly checkOut)
        {
            var bookedRoomIds = await _context.BookingDetails
                .Where(d => d.Booking.CheckInDate < checkOut && d.Booking.CheckOutDate > checkIn)
                .Select(d => d.RoomId)
                .Distinct()
                .ToListAsync();

            var availableRooms = await _context.Rooms
                .Where(r => !bookedRoomIds.Contains(r.RoomId) && r.RoomStatus == "available")
                .ToListAsync();

            return Ok(availableRooms);
        }
        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmBooking([FromBody] ConfirmBookingDTO request)
        {
            if (request.RoomIds == null || request.RoomIds.Count == 0)
            {
                return BadRequest("Must include at least one room.");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
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
                await _context.SaveChangesAsync(); // BookingId is generated here

                foreach (var roomId in request.RoomIds)
                {
                    var detail = new BookingDetail
                    {
                        BookingId = booking.BookingId,
                        RoomId = roomId
                    };
                    _context.BookingDetails.Add(detail);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new
                {
                    Message = "Booking confirmed.",
                    BookingId = booking.BookingId
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
