using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Models;

[Table("ParkingService")]
public partial class ParkingService
{
    [Key]
    [Column("parkingServiceId")]
    public int ParkingServiceId { get; set; }

    [Column("bookingId")]
    public int BookingId { get; set; }

    [Column("parkingPlateNo")]
    [StringLength(20)]
    public string ParkingPlateNo { get; set; } = null!;

    [ForeignKey("BookingId")]
    [InverseProperty("ParkingServices")]
    public virtual Booking Booking { get; set; } = null!;
}
