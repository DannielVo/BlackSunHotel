using System;
using System.Collections.Generic;

namespace HotelManagement.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public string RoomTitle { get; set; } = null!;

    public int RoomTypeId { get; set; }

    public string? RoomDescription { get; set; }

    public string? RoomImage { get; set; }

    public string RoomStatus { get; set; } = null!;

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<RoomService> RoomServices { get; set; } = new List<RoomService>();

    public virtual RoomType RoomType { get; set; } = null!;
}
