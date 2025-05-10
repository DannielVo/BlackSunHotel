using System;
using System.Collections.Generic;

namespace HotelManagement.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Fullname { get; set; } = null!;

    public string? Email { get; set; }

    public string Phone { get; set; } = null!;

    public bool IsStaff { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    public string PasswordHash { get; set; } = null!;
}
