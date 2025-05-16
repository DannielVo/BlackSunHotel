namespace HotelManagement.DTOs
{
    public class UsersRequestDTO
    {
        public string Fullname { get; set; }
        public string? Email { get; set; }
        public string Phone { get; set; }
        public bool IsStaff { get; set; }
        public string? RoleName { get; set; }
        public string Password { get; set; }
    }
}
