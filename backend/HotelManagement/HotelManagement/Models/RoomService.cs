using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    // Models/RoomService.cs
    public class RoomService
    {
        public int RoomServiceId { get; set; }

        [Required]
        public int RoomId { get; set; } // FK -> Rooms.RoomId

        [Required]
        public DateTime DateTime { get; set; } // Thời gian yêu cầu dọn phòng

        public bool IsCleaningDone { get; set; } = false; // Mặc định chưa hoàn thành

        // Navigation property
        public Room Room { get; set; }
    }
}
