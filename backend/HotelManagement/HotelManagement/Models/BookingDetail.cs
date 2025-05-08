using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class BookingDetail
    {
        public int BookingDetailId { get; set; } // Primary Key (PK)

        [Required]
        public int BookingId { get; set; } // Foreign Key (FK) -> Bookings.BookingId

        [Required]
        public int RoomId { get; set; } // Foreign Key (FK) -> Rooms.RoomId

        // Navigation properties
        public Booking Booking { get; set; } // Chi tiết thuộc về 1 Booking
        public Room Room { get; set; } // Chi tiết thuộc về 1 Room
    }
}
