using System.ComponentModel.DataAnnotations;

namespace HotelManagement.DTOs
{
    public class BookingResponseDTO
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }

        [Required]
        public string Fullname { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal TotalPrice { get; set; }

        public string PaymentStatus { get; set; }
        public List<BookingDetailResponseDTO> BookingDetails { get; set; }
    }

    public class BookingDetailResponseDTO
    {
        public int RoomId { get; set; }
        public string RoomTitle { get; set; }
        public string RoomType { get; set; }
    }

    public class CreateBookingDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Fullname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal TotalPrice { get; set; }
    }

    public class UpdateBookingDTO
    {
        public int BookingId { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateOnly? CheckInDate { get; set; }
        public DateOnly? CheckOutDate { get; set; }
        public decimal? TotalPrice { get; set; }
    }

    public class CheckAvailabilityRequestDTO
    {
        public DateOnly CheckIn { get; set; }
        public DateOnly CheckOut { get; set; }
    }

    public class AvailableRoomResponseDTO
    {
        public int RoomId { get; set; }
        public string RoomTitle { get; set; }
        public string RoomDescription { get; set; }
        public string RoomType { get; set; }
        public decimal PricePerNight { get; set; }
        public string RoomStatus { get; set; }
    }

    public class ConfirmBookingDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Fullname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal TotalPrice { get; set; }

        [Required]
        public List<int> RoomIds { get; set; }
    }

    public class ConfirmBookingResponseDTO
    {
        public string Message { get; set; }
        public int BookingId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<int> BookedRoomIds { get; set; }
    }

    public class ErrorResponseDTO
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}