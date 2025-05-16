namespace HotelManagement.DTOs
{
    public class RoomServicesDTO
    {
        public int RoomServiceId { get; set; }
        public int RoomId { get; set; }
        public DateTime ServiceDateTime { get; set; }
        public string RoomServiceStatus { get; set; }
    }
}
