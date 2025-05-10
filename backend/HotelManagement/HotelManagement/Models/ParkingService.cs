using System;
using System.Collections.Generic;

namespace HotelManagement.Models;

public partial class ParkingService
{
    public int ParkingServiceId { get; set; }

    public int BookingId { get; set; }

    public string ParkingPlateNo { get; set; } = null!;

    public virtual Booking Booking { get; set; } = null!;
}
