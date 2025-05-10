using System;
using System.Collections.Generic;

namespace HotelManagement.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int RoomId { get; set; }

    public int UserId { get; set; }

    public int BookingId { get; set; }

    public string? ReviewContent { get; set; }

    public int Rating { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
