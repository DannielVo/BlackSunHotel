using System.ComponentModel.DataAnnotations;

namespace HotelManagement.DTOs
{
    // Request DTO
    public class CreateReviewDTO
    {
        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Booking ID is required")]
        public int BookingId { get; set; }

        [StringLength(500, ErrorMessage = "Review cannot exceed 500 characters")]
        public string? ReviewContent { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }
    }

    // Response DTO
    public class ReviewResponseDTO
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public string UserFullname { get; set; } // Added for richer response
        public int BookingId { get; set; }
        public string? ReviewContent { get; set; }
        public int Rating { get; set; }
    }
}