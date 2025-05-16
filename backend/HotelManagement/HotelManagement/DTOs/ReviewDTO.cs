namespace HotelManagement.DTOs
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int BookingId { get; set; }
        public string? ReviewContent { get; set; }
        public int Rating { get; set; }
    }
}
