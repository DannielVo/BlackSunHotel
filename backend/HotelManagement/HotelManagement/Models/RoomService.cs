using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Models;

[Table("RoomService")]
public partial class RoomService
{
    [Key]
    [Column("roomServiceId")]
    public int RoomServiceId { get; set; }

    [Column("roomId")]
    public int RoomId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ServiceDateTime { get; set; }

    [Column("isCleaningDone")]
    public bool IsCleaningDone { get; set; }

    [ForeignKey("RoomId")]
    [InverseProperty("RoomServices")]
    public virtual Room Room { get; set; } = null!;
}
