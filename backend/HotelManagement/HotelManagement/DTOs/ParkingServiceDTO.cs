using System.ComponentModel.DataAnnotations;

namespace HotelManagement.DTOs
{
    public class ParkingServiceResponseDTO
    {
        public int ParkingServiceId { get; set; }
        public int BookingId { get; set; }
        public string ParkingPlateNo { get; set; }
    }

    public class CreateParkingServiceDTO
    {
        [Required(ErrorMessage = "Booking ID is required")]
        public int BookingId { get; set; }

        [Required(ErrorMessage = "License plate number is required")]
        [StringLength(20, ErrorMessage = "Plate number cannot exceed 20 characters")]
        public string ParkingPlateNo { get; set; }
    }

    public class UpdateParkingServiceDTO
    {
        [Required]
        public int ParkingServiceId { get; set; }

        [StringLength(20, ErrorMessage = "Plate number cannot exceed 20 characters")]
        public string? ParkingPlateNo { get; set; }
    }
}