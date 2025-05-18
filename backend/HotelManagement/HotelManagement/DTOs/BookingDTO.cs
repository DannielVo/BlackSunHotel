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
        public decimal PricePerNight { get; set; } // Added for transparency
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

        [DateAfterToday]
        public DateOnly CheckInDate { get; set; }

        [DateAfter(nameof(CheckInDate))]
        public DateOnly CheckOutDate { get; set; }

        // TotalPrice removed; calculated server-side
    }

    public class UpdateBookingDTO
    {
        public int BookingId { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateOnly? CheckInDate { get; set; }
        public DateOnly? CheckOutDate { get; set; }
        public decimal? TotalPrice { get; set; }
    }

    public class CheckAvailabilityRequestDTO
    {
        [DateAfterToday]
        public DateOnly CheckIn { get; set; }

        [DateAfter(nameof(CheckIn))]
        public DateOnly CheckOut { get; set; }
    }

    public class AvailableRoomResponseDTO
    {
        public int RoomId { get; set; }
        public string RoomTitle { get; set; }
        public string RoomDescription { get; set; }
        public string RoomType { get; set; }
        public decimal PricePerNight { get; set; }
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

        [DateAfterToday]
        public DateOnly CheckInDate { get; set; }

        [DateAfter(nameof(CheckInDate))]
        public DateOnly CheckOutDate { get; set; }

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

    // Custom Validation Attribute
    public class DateAfterAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;
        public DateAfterAttribute(string comparisonProperty) => _comparisonProperty = comparisonProperty;

        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            var earlierDate = context.ObjectInstance.GetType().GetProperty(_comparisonProperty)?.GetValue(context.ObjectInstance);
            if (value is DateOnly laterDate && earlierDate is DateOnly)
            {
                if (laterDate > (DateOnly)earlierDate) return ValidationResult.Success;
                return new ValidationResult($"{context.DisplayName} must be after {_comparisonProperty}.");
            }
            return ValidationResult.Success;
        }
    }

    public class DateAfterTodayAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value is DateOnly date && date < DateOnly.FromDateTime(DateTime.Today))
                return new ValidationResult($"{context.DisplayName} must be in the future.");
            return ValidationResult.Success;
        }
    }
}