using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    // Models/ParkingService.cs
    public class ParkingService
    {
        public int ParkingServiceId { get; set; }

        [Required]
        public int BookingId { get; set; } // FK -> Bookings.BookingId

        [Required(ErrorMessage = "Biển số xe không được để trống.")]
        public string ParkingPlateNo { get; set; } // Ví dụ: "51G-12345"

        // Navigation property
        public Booking Booking { get; set; }
    }
}
