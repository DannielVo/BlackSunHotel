using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Models;

public partial class Review
{
    [Key]
    [Column("reviewId")]
    public int ReviewId { get; set; }

    [Column("userId")]
    public int UserId { get; set; }

    [Column("bookingId")]
    public int BookingId { get; set; }

    [Column("reviewContent")]
    public string? ReviewContent { get; set; }

    [Column("rating")]
    public int Rating { get; set; }

    [ForeignKey("BookingId")]
    [InverseProperty("Reviews")]
    public virtual Booking Booking { get; set; } = null!;

    [ForeignKey("RoomId")]
    [InverseProperty("Reviews")]
    public virtual Room Room { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Reviews")]
    public virtual User User { get; set; } = null!;
}
