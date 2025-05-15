namespace HotelManagement.DTOs
{
    public class RegisterUserDTO
    {
        public string Fullname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

}
