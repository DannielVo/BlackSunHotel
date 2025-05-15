using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Models;

public partial class User
{
    [Key]
    [Column("userId")]
    public int UserId { get; set; }

    [Column("fullname")]
    [StringLength(255)]
    public string Fullname { get; set; } = null!;

    [Column("email")]
    [StringLength(255)]
    public string? Email { get; set; }

    [Column("phone")]
    [StringLength(20)]
    public string Phone { get; set; } = null!;

    [Column("isStaff")]
    public bool IsStaff { get; set; }

    [Column("roleName")]
    [StringLength(50)]
    public string? RoleName { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    [InverseProperty("User")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    [Column("password")]
    [StringLength(255)]
    public string Password { get; set; } = null!;
}
