using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Models;

public partial class Booking
{
    [Key]
    [Column("bookingId")]
    public int BookingId { get; set; }

    [Column("userId")]
    public int UserId { get; set; } // Changed to non-nullable

    [Column("fullname")]
    [StringLength(255)]
    public string Fullname { get; set; }

    [Column("email")]
    [StringLength(255)]
    public string Email { get; set; }

    [Column("phone")]
    [StringLength(20)]
    public string Phone { get; set; }

    [Column("checkInDate")]
    public DateOnly CheckInDate { get; set; }

    [Column("checkOutDate")]
    public DateOnly CheckOutDate { get; set; }

    [Column("totalPrice", TypeName = "decimal(10, 2)")]
    public decimal TotalPrice { get; set; }

    [Column("paymentStatus")]
    [StringLength(50)]
    public string PaymentStatus { get; set; } = "pending";

    [Timestamp]
    public byte[] RowVersion { get; set; } // Concurrency token

    [InverseProperty("Booking")]
    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    [InverseProperty("Booking")]
    public virtual ICollection<ParkingService> ParkingServices { get; set; } = new List<ParkingService>();

    [InverseProperty("Booking")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [ForeignKey("UserId")]
    [InverseProperty("Bookings")]
    public virtual User User { get; set; }
}