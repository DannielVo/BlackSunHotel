namespace HotelManagement.DTOs
{
    public class RoomDTO
    {
        public int RoomId { get; set; }
        public string RoomTitle { get; set; }
        public int RoomTypeId { get; set; }
        public string? RoomDescription { get; set; }
        public string? RoomImage { get; set; }
        public string RoomStatus { get; set; }
    }
}
