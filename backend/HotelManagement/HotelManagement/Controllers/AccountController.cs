using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace NTN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly CustomerContext _context;

        public AccountController(CustomerContext context)
        {
            _context = context;
        }

        // Đăng ký
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegistrationRequest request)
        {
            // Kiểm tra email tồn tại
            if (_context.Users.Any(u => u.Email == request.Email))
            {
                return BadRequest(new { Message = "Email đã tồn tại" });
            }

            // Tạo user mới
            var user = new User
            {
                Fullname = request.Fullname,
                Email = request.Email,
                Phone = request.Phone,
                PasswordHash = HashPassword(request.Password)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new { Message = "Đăng ký thành công" });
        }

        // Đăng nhập
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);

            if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
            {
                return Unauthorized(new { Message = "Email hoặc mật khẩu không đúng" });
            }

            // Tạo session
            HttpContext.Session.SetInt32("UserId", user.UserId);
            return Ok(new { Message = "Đăng nhập thành công" });
        }

        // Đăng xuất
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Ok(new { Message = "Đã đăng xuất" });
        }

        // Hàm băm mật khẩu (SHA256)
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        // Xác minh mật khẩu
        private bool VerifyPassword(string password, string storedHash)
        {
            return HashPassword(password) == storedHash;
        }
    }

    // DTO cho đăng ký
    public class UserRegistrationRequest
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }

    // DTO cho đăng nhập
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}