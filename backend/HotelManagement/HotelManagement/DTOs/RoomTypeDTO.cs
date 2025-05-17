using System.ComponentModel.DataAnnotations;

namespace HotelManagement.DTOs
{
    public class RoomTypeDTO
    {
        public int RoomTypeId { get; set; }

        [Required(ErrorMessage = "Room type name is required")]
        [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters")]
        public string RoomTypeName { get; set; }

        public string? RoomDesc { get; set; }
        public string? RoomFeatures { get; set; }
        public string? RoomAmenities { get; set; }
        public string? RoomImg { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be positive")]
        public decimal RoomPrice { get; set; }
    }
}