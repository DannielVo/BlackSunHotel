using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required] public string FullName { get; set; }
        [EmailAddress] public string Email { get; set; }
        [Required] public string Password { get; set; }
        public bool IsStaff { get; set; }
        public string RoleName { get; set; } = "customer";
    }
}
