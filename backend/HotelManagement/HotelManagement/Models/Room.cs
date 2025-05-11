using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Models;

public partial class Room
{
    [Key]
    [Column("roomId")]
    public int RoomId { get; set; }

    [Column("roomTitle")]
    [StringLength(100)]
    public string RoomTitle { get; set; } = null!;

    [Column("roomTypeId")]
    public int RoomTypeId { get; set; }

    [Column("roomDescription")]
    public string? RoomDescription { get; set; }

    [Column("roomImage")]
    [StringLength(255)]
    public string? RoomImage { get; set; }

    [Column("roomStatus")]
    [StringLength(50)]
    public string RoomStatus { get; set; } = null!;

    [InverseProperty("Room")]
    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    [InverseProperty("Room")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [InverseProperty("Room")]
    public virtual ICollection<RoomService> RoomServices { get; set; } = new List<RoomService>();

    [ForeignKey("RoomTypeId")]
    [InverseProperty("Rooms")]
    public virtual RoomType RoomType { get; set; } = null!;
}
