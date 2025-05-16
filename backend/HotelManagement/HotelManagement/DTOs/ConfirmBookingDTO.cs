namespace HotelManagement.DTOs
{
    public class ConfirmBookingDTO
    {
        public int? UserId { get; set; }
        public string Fullname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<int> RoomIds { get; set; } = new();
    }
}
