namespace HotelManagement.DTOs
{
    public class RoomTypeDTO
    {
        public int RoomTypeId { get; set; }
        public string? RoomDesc { get; set; }
        public string? RoomFeatures { get; set; }
        public string? RoomAmenities { get; set; }
        public string? RoomImg { get; set; }
        public decimal RoomPrice { get; set; }
    }
}
