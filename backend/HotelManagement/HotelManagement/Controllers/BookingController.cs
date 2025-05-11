using Microsoft.AspNetCore.Mvc;
using HotelManagement.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly DbAccount _context;

        public BookingController(DbAccount context)
        {
            _context = context;
        }

        // GET: api/booking/rooms
        [HttpGet("rooms")]
        public ActionResult<IEnumerable<Room>> GetAvailableRooms()
        {
            var rooms = _context.Rooms
                .Include(r => r.RoomType)
                .Where(r => r.RoomStatus == "available")
                .ToList();

            return Ok(rooms);
        }

        // POST: api/booking/create
        [HttpPost("create")]
        public ActionResult<Booking> CreateBooking([FromBody] BookingCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Kiểm tra phòng còn trống
            var isRoomAvailable = !_context.Bookings
                .Any(b => b.BookingDetails.Any(bd => bd.RoomId == request.RoomId) &&
                           request.CheckInDate < b.CheckOutDate &&
                           request.CheckOutDate > b.CheckInDate);

            if (!isRoomAvailable)
            {
                return BadRequest("Phòng đã được đặt trong khoảng thời gian này.");
            }

            try
            {
                // Tạo booking
                var booking = new Booking
                {
                    UserId = request.UserId,
                    CheckInDate = request.CheckInDate,
                    CheckOutDate = request.CheckOutDate,
                    TotalPrice = CalculateTotalPrice(request.RoomId, request.CheckInDate, request.CheckOutDate),
                    PaymentStatus = "pending"
                };

                _context.Bookings.Add(booking);
                _context.SaveChanges();

                // Thêm chi tiết booking
                var bookingDetail = new BookingDetail
                {
                    BookingId = booking.BookingId,
                    RoomId = request.RoomId
                };

                _context.BookingDetails.Add(bookingDetail);
                _context.SaveChanges();

                return Ok(booking);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        // GET: api/booking/check-availability?roomId=1&checkIn=2023-10-01&checkOut=2023-10-05
        [HttpGet("check-availability")]
        public ActionResult<bool> CheckAvailability(
            [FromQuery] int roomId,
            [FromQuery] DateOnly checkIn,
            [FromQuery] DateOnly checkOut)
        {
            var isAvailable = !_context.Bookings
                .Any(b => b.BookingDetails.Any(bd => bd.RoomId == roomId) &&
                           checkIn < b.CheckOutDate &&
                           checkOut > b.CheckInDate);

            return Ok(isAvailable);
        }

        private decimal CalculateTotalPrice(int roomId, DateOnly checkIn, DateOnly checkOut)
        {
            var days = (checkOut.ToDateTime(TimeOnly.MinValue) - checkIn.ToDateTime(TimeOnly.MinValue)).Days;
            var room = _context.Rooms
                .Include(r => r.RoomType)
                .FirstOrDefault(r => r.RoomId == roomId);

            if (room == null)
            {
                throw new ArgumentException("Phòng không tồn tại.");
            }

            return days * room.RoomType.RoomPrice;
        }
    }

    public class BookingCreateRequest
    {
        [Required]
        public int RoomId { get; set; }

        public int? UserId { get; set; }

        [Required]
        public DateOnly CheckInDate { get; set; }

        [Required]
        public DateOnly CheckOutDate { get; set; }
    }
}