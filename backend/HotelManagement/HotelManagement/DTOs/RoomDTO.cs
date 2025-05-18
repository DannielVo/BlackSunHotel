using System.ComponentModel.DataAnnotations;

namespace HotelManagement.DTOs
{
    public class RoomDTO
    {
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Room title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string RoomTitle { get; set; }

        [Required(ErrorMessage = "Room type is required")]
        public int RoomTypeId { get; set; }

        public string? RoomDescription { get; set; }
        public string? RoomImage { get; set; }

        [Required(ErrorMessage = "Room status is required")]
        [RegularExpression("available|occupied|maintenance",
            ErrorMessage = "Status must be 'available', 'occupied', or 'maintenance'")]
        public string RoomStatus { get; set; }

        // Include RoomType details for richer responses
        public string? RoomTypeName { get; set; }
        public decimal? RoomTypePrice { get; set; }
    }
}