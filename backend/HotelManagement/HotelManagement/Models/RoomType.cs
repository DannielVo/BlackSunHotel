using System;
using System.Collections.Generic;

namespace HotelManagement.Models;

public partial class RoomType
{
    public int RoomTypeId { get; set; }

    public string? RoomDesc { get; set; }

    public string? RoomFeatures { get; set; }

    public string? RoomAmenities { get; set; }

    public string? RoomImg { get; set; }

    public decimal RoomPrice { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
