namespace HotelManagement.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int? UserId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaymentStatus { get; set; } = "pending";
        public List<BookingDetail> BookingDetails { get; set; } = new();
    }
}
