using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Models;

public partial class BookingDetail
{
    [Key]
    [Column("bookingDetailId")]
    public int BookingDetailId { get; set; }

    [Column("bookingId")]
    public int BookingId { get; set; }

    [Column("roomId")]
    public int RoomId { get; set; }

    [ForeignKey("BookingId")]
    [InverseProperty("BookingDetails")]
    public virtual Booking Booking { get; set; } = null!;

    [ForeignKey("RoomId")]
    [InverseProperty("BookingDetails")]
    public virtual Room Room { get; set; } = null!;
}
