using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        [Required]
        public int RoomId { get; set; } // FK -> Rooms.RoomId

        [Required]
        public int UserId { get; set; } // FK -> Users.UserId

        [Required]
        public int BookingId { get; set; } // FK -> Bookings.BookingId

        [Required(ErrorMessage = "Nội dung đánh giá không được để trống.")]
        public string ReviewContent { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Đánh giá phải từ 1 đến 5 sao.")]
        public int Rating { get; set; } // 1-5 sao

        // Navigation properties
        public Room Room { get; set; }
        public User User { get; set; }
        public Booking Booking { get; set; }
    }
}
