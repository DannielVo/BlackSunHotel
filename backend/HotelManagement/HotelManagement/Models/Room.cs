using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        [Required] public string RoomTitle { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomStatus { get; set; } = "available";
    }
}
