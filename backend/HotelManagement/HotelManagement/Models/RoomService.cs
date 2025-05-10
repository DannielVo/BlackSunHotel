using System;
using System.Collections.Generic;

namespace HotelManagement.Models;

public partial class RoomService
{
    public int RoomServiceId { get; set; }

    public int RoomId { get; set; }

    public DateTime ServiceDateTime { get; set; }

    public bool IsCleaningDone { get; set; }

    public virtual Room Room { get; set; } = null!;
}
